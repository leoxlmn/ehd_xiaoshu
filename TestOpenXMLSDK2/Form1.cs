using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using FilePathExtender;
using WordFileProcessor;


namespace TestOpenXMLSDK2 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e) {
            try {
                //Get app root path
                string AppRootPath = @"C:\";
                string tmpPath = Path.GetTempPath();

                //1) Get the word template file path
                string templateFilePath = FileExtender.ConcatPhysicalPath(AppRootPath,
                    @"Test",
                    @"testTemplate.docx");

                //2) Create a new temp word file which will be populated from the template and actual treatment data
                string destFilePath =
                    FileExtender.ConcatPhysicalPath(tmpPath,
                    //Constants.CFG_PRINTOUT_FOLDER,
                    Guid.NewGuid().ToString() + ".docx");

                //3) Construct a ProcessRequest object, which will be passed to RequestProcessor to populate the temp word file
                ProcessRequest request = new ProcessRequest(destFilePath, templateFilePath, false);
                request.AddWordReplacement("{title}", "[replaced]");
                //populate request


                //4) Call RequestProcessor to populate word file
                RequestProcessor processor = new RequestProcessor(null);
                /**
                 * Using OpenXMLSDK 2.0 is alot faster than using office dlls
                 */
                //ProcessResponse response = processor.BuildWordFromTemplate(request, _progressReporter);
                ProcessResponse response = processor.OpenXMLBuildWordFromTemplate(request, null);
                if (!response.Succeeded) {
                    MessageBox.Show(this, "Failed to create Word file. " + response.ReturnMessage);
                    return;
                } else {
                    MessageBox.Show(this, destFilePath);
                }

            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to create Word file. " + ex.Message);
            }
        }

        private OpenXmlElement findElementByText(OpenXmlElement ParentElement, string text) {

            OpenXmlElement found = null;

            foreach(OpenXmlElement element in ParentElement.Elements()) {
                if(element is Text && element.InnerText.Equals(text)) {
                    return element;
                } else if(element.HasChildren){
                    found = findElementByText(element, text);
                    if(found != null) return found;
                }
            }

            return null;
        }


        public static void InsertAPicture(WordprocessingDocument wordprocessingDocument, OpenXmlElement container, string fileName) {
            MainDocumentPart mainPart = wordprocessingDocument.MainDocumentPart;

            ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);

            using(FileStream stream = new FileStream(fileName, FileMode.Open)) {
                imagePart.FeedData(stream);
            }

            AddImageToBody(container, mainPart.GetIdOfPart(imagePart));
        }

        private static void AddImageToBody(OpenXmlElement container, string relationshipId) {

            long imgWidth = 1890000L;
            long imgHeight = 792000L;

            //Define the reference of the image.
            var element =
                 new Drawing(
                     new DW.Inline(
                         new DW.Extent() { Cx = imgWidth, Cy = imgHeight },
                         new DW.EffectExtent() {
                             LeftEdge = 0L, TopEdge = 0L,
                             RightEdge = 0L, BottomEdge = 0L
                         },
                         new DW.DocProperties() {
                             Id = (UInt32Value)1U,
                             Name = "Picture 1"
                         },
                         new DW.NonVisualGraphicFrameDrawingProperties(
                             new A.GraphicFrameLocks() { NoChangeAspect = true }),
                         new A.Graphic(
                             new A.GraphicData(
                                 new PIC.Picture(
                                     new PIC.NonVisualPictureProperties(
                                         new PIC.NonVisualDrawingProperties() {
                                             Id = (UInt32Value)0U,
                                             Name = "New Bitmap Image.jpg"
                                         },
                                         new PIC.NonVisualPictureDrawingProperties()),
                                     new PIC.BlipFill(
                                         new A.Blip(
                                             new A.BlipExtensionList(
                                                 new A.BlipExtension() {
                                                     Uri =
                                                       "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                                 })
                                         ) {
                                             Embed = relationshipId,
                                             CompressionState =
                                             //A.BlipCompressionValues.Print
                                             A.BlipCompressionValues.HighQualityPrint
                                         },
                                         new A.Stretch(
                                             new A.FillRectangle())),
                                     new PIC.ShapeProperties(
                                         new A.Transform2D(
                                             new A.Offset() { X = 0L, Y = 0L },
                                             new A.Extents() { Cx = imgWidth, Cy = imgHeight }),
                                         new A.PresetGeometry(
                                             new A.AdjustValueList()
                                         ) { Preset = A.ShapeTypeValues.Rectangle }))
                             ) { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                     ) {
                         DistanceFromTop = (UInt32Value)0U,
                         DistanceFromBottom = (UInt32Value)0U,
                         DistanceFromLeft = (UInt32Value)0U,
                         DistanceFromRight = (UInt32Value)0U
                         //, EditId = "50D07946"
                     });

            //Append the reference to body, the element should be in a Run.
            while(!(container is Paragraph)) {
                container = container.Parent;
            }
            container.AppendChild(new Paragraph(new Run(element)));
        }

    }
}
