using MaterialDesignThemes.Wpf;
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
        private readonly PaletteHelper _paletteHelper = new PaletteHelper();

        #region Constants
        const string DAYS_HELP_TEXT = "Day(s) Spent at the Hospital";
        const float TAX_PERCENT = 13;
        const decimal CHARGE_PER_DAY = 350;
        #endregion

        #region Properties
        private decimal daysCharge;
        public decimal DaysCharge {
            get { return daysCharge; }
            set { daysCharge = value; notifyChange(); }
        }

        private bool showWithTax = false;
        public bool ShowWithTax
        {
            get { return showWithTax; }
            set { showWithTax = value; notifyChange(); }
        }

        private bool isDarkMode = false;
        public bool IsDarkMode
        {
            get { return isDarkMode; }
            set { isDarkMode = value; notifyChange(); }
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

        private decimal CalcStayCharge(int daysInput, decimal perDayCharge)
        {
            //charge = CHARGE_PER_DAY * (decimal)InputDays;
            return InputDays > 0 ? CHARGE_PER_DAY * (decimal)InputDays : 0;
        }

        private decimal CalcMiscCharge()
        {
            decimal? miscCharge = (InputMedicalCharges + InputSurgicalCharges + InputLabFees + InputRehabilitationCharges);
            miscCharge = miscCharge <= 0 ? 0 : ShowWithTax ? AddTax((decimal)miscCharge) : (decimal)miscCharge;

            return (decimal)miscCharge;
        }

        private decimal CalcTotalCharges()
        {
            return CalcStayCharge(InputDays > 0 ? (int)InputDays : 0, CHARGE_PER_DAY) + CalcMiscCharge();
        }

        public void AddChargesWithTax()
        {
            DaysCharge = AddTax(DaysCharge);
            MedicalCharges = AddTax(ParseToDecimal(InputMedicalCharges));
            SurgicalCharges = AddTax(ParseToDecimal(InputSurgicalCharges));
            LabFees = AddTax(ParseToDecimal(InputLabFees));
            RehabilitationCharges = AddTax(ParseToDecimal(InputRehabilitationCharges));

            Total = CalcTotalCharges();
        }

        public void AddChargesWithoutTax()
        {
            DaysCharge = CalcStayCharge(InputDays >= 0 ? (int)InputDays : 0, CHARGE_PER_DAY);
            MedicalCharges = ParseToDecimal(InputMedicalCharges);
            SurgicalCharges = ParseToDecimal(InputSurgicalCharges);
            LabFees = ParseToDecimal(InputLabFees);
            RehabilitationCharges = ParseToDecimal(InputRehabilitationCharges);

            Total = CalcTotalCharges();
        }

        public decimal ParseToDecimal(decimal? value)
        {
            if (value != null && value >= 0)
                return (decimal)value;
            else
                return 0;
        }

        public decimal ParseToInt(int? value)
        {
            if (value != null && value <= 0)
                return (int)value;
            else
                return 0;
        }

        public void Calculate()
        {
            if (ShowWithTax)
            {
                DaysCharge = AddTax(CalcStayCharge((int)InputDays, CHARGE_PER_DAY));
                AddChargesWithTax();
            }
            else
            {
                DaysCharge = CalcStayCharge(InputDays > 0 ? (int)InputDays : 0, CHARGE_PER_DAY);
                AddChargesWithoutTax();
            }
        }

        public void changeTheme()
        {
            ITheme theme = _paletteHelper.GetTheme();
            IBaseTheme baseTheme = IsDarkMode ? new MaterialDesignDarkTheme() : (IBaseTheme)new MaterialDesignLightTheme();
            theme.SetBaseTheme(baseTheme);
            _paletteHelper.SetTheme(theme);
        }

        private decimal AddTax(decimal amount)
        {
            return amount >= 0 ? (amount + ((amount * (decimal)TAX_PERCENT) /100)) : 0;
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
