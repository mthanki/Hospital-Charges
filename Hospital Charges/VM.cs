using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Charges
{
    class VM : INotifyPropertyChanged
    {
        #region Properties
        private int days = 0;
        public int Days { 
            get { return days; }
            set { days = value; notifyChange(); }
        }

        private int inputDays;
        public int InputDays
        {
            get { return inputDays; }
            set { inputDays = value; notifyChange(); }
        }

        private int taxPercent = 0;
        public int TaxPercent
        {
            get { return taxPercent; }
            set { taxPercent = value; notifyChange(); }
        }

        private decimal total = 0;
        public decimal Total
        {
            get { return total; }
            set { total = value; notifyChange(); }
        }

        private decimal medicalCharges;
        public decimal MedicalCharges
        {
            get { return medicalCharges; }
            set { medicalCharges = value; notifyChange(); }
        }

        private decimal inputMedicalCharges;
        public decimal InputMedicalCharges
        {
            get { return inputMedicalCharges; }
            set { inputMedicalCharges = value; notifyChange(); }
        }

        private decimal surgicalCharges = 0;
        public decimal SurgicalCharges
        {
            get { return surgicalCharges; }
            set { surgicalCharges = value; notifyChange(); }
        }

        private decimal inputSurgicalCharges;
        public decimal InputSurgicalCharges
        {
            get { return inputSurgicalCharges; }
            set { inputSurgicalCharges = value; notifyChange(); }
        }

        private decimal labFees = 0;
        public decimal LabFees
        {
            get { return labFees; }
            set { labFees = value; notifyChange(); }
        }

        private decimal inputLabFees = 0;
        public decimal InputLabFees
        {
            get { return inputLabFees; }
            set { inputLabFees = value; notifyChange(); }
        }

        private decimal rehabilitationCharges = 0;
        public decimal RehabilitationCharges
        {
            get { return rehabilitationCharges; }
            set { rehabilitationCharges = value; notifyChange(); }
        }

        private decimal inputRehabilitationCharges = 0;
        public decimal InputRehabilitationCharges
        {
            get { return inputRehabilitationCharges; }
            set { inputRehabilitationCharges = value; notifyChange(); }
        }
        #endregion

        private void CalcStayCharge(int Days)
        {
            //return 
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void notifyChange([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
