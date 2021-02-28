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
        #region Constants
        const string DAYS_HELP_TEXT = "Day(s) Spent at the Hospital";
        const float TAX_PERCENT = 13;
        #endregion

        #region Properties
        private decimal daysCharge = 350;
        public decimal DaysCharge { 
            get { return daysCharge; }
            set { daysCharge = value; notifyChange(); }
        }

        private int? inputDays;
        public int? InputDays
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

        private decimal? inputMedicalCharges;
        public decimal? InputMedicalCharges
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

        private decimal? inputSurgicalCharges;
        public decimal? InputSurgicalCharges
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

        private decimal? inputLabFees;
        public decimal? InputLabFees
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

        private decimal? inputRehabilitationCharges;
        public decimal? InputRehabilitationCharges
        {
            get { return inputRehabilitationCharges; }
            set { inputRehabilitationCharges = value; notifyChange(); }
        }
        #endregion

        public decimal CalcStayCharge()
        {
            decimal charge = DaysCharge * (decimal)InputDays;
            charge = AddTax(charge);

            return charge;
        }

        private decimal CalcMiscCharge()
        {
            decimal? miscCharge = (InputMedicalCharges + InputSurgicalCharges + InputLabFees + InputRehabilitationCharges);
            miscCharge = AddTax((decimal)miscCharge);

            return (decimal)miscCharge;
        }

        private decimal CalcTotalCharges()
        {
            return CalcStayCharge() + CalcMiscCharge();
        }

        private decimal AddTax(decimal amount)
        {
            return amount != 0 ? (amount + ((amount * (decimal)TAX_PERCENT) /100)) : 0;
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
