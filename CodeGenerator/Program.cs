using System;
using System.IO;
using MySql.Data.MySqlClient;

namespace CodeGenerator {
    class Program {
        static void Main(string[] args) {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            StreamWriter writerEntity = null;
            StreamWriter writerMap = null;
            string connectionString = "Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;";
            string tableName = "naturopathicdetail";
            string outputEntityFileName = "outputEntity.txt";
            string lineEntityTemplate = @"public virtual [ReturnType] [PropertyName] {get; set;}";
            string outputMapFileName = "outputMap.txt";
            string lineMapTemplate = "Map(x => x.[PropertyName]).Column(\"[ColumnName]\");";

            try {
                conn = new MySqlConnection(connectionString);
                conn.Open();

                //1) Get all column names
                string sql = "SELECT * FROM " + tableName + " WHERE 1<0;";
                cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();

                //2) Create an output file
                writerEntity = File.CreateText(outputEntityFileName);
                writerMap = File.CreateText(outputMapFileName);

                //3) For each column
                string columnTypeName = "string";
                string columnName = string.Empty;
                string propertyName = string.Empty;
                int numOfColumns = reader.FieldCount;

                for(int i = 0; i < numOfColumns; i++) {
                    columnName = reader.GetName(i);
                    columnTypeName = reader.GetDataTypeName(i);
                    propertyName = columnName.Replace("NPC_NPC", "").Replace("NPC_","");
                    switch(columnTypeName) {
                        case "INT": 
                            columnTypeName = "int";
                            break;
                        case "BIT": 
                            columnTypeName = "bool";
                            break;
                        case "TEXT": 
                        case "VARCHAR":
                            columnTypeName = "string";
                            break;
                        case "DATETIME":
                            columnTypeName = "DateTime";
                            break;
                        default:
                            columnTypeName = "string";
                            break;
                    }
                    writerEntity.WriteLine(lineEntityTemplate.Replace("[ReturnType]", columnTypeName)
                        .Replace("[PropertyName]", propertyName)
                        );
                    writerMap.WriteLine(lineMapTemplate.Replace("[PropertyName]", propertyName)
                        .Replace("[ColumnName]", columnName)
                        );
                }


            } catch(Exception ex) {
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine("Trace: ");
                Console.WriteLine(ex.StackTrace);
            } finally {
                if(writerEntity != null) {
                    writerEntity.Flush();
                    writerEntity.Close();
                    writerEntity = null;
                }
                if(writerMap != null) {
                    writerMap.Flush();
                    writerMap.Close();
                    writerMap = null;
                }
                if(reader != null && !reader.IsClosed) {
                    reader.Close();
                    reader = null;
                }
                if(cmd != null) {
                    cmd = null;
                }
                if(conn != null) {
                    conn.Close();
                    conn = null;
                }
            }
        }

    }
}
