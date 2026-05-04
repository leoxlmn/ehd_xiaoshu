using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Windows.Forms;
using EHD.Model.Entity;

namespace EHD.Admin {
    public interface ITreatmentDetailUserControl {
        ITreatmentDetail TreatmentDetail { get; }
        void Save(ISession session);
        void LoadFromTemplate(ITreatmentDetail templateDetail);
    }
}
