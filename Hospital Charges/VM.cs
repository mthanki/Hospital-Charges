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

        private decimal daysChargeTax;
        public decimal DaysChargeTax
        {
            get { return daysChargeTax; }
            set { daysChargeTax = value; notifyChange(); }
        }

        private decimal miscCharge;
        public decimal MiscCharge
        {
            get { return miscCharge; }
            set { miscCharge = value; notifyChange(); }
        }

        private int taxPercent = 0;
        public int TaxPercent
        {
            get { return taxPercent; }
            set { taxPercent = value; notifyChange(); }
        }

        private decimal totalTax = 0;
        public decimal TotalTax
        {
            get { return totalTax; }
            set { totalTax = value; notifyChange(); }
        }

        private decimal totalWithTax = 0;
        public decimal TotalWithTax
        {
            get { return totalWithTax; }
            set { totalWithTax = value; notifyChange(); }
        }

        private decimal? inputMedicalCharges;
        public decimal? InputMedicalCharges
        {
            get { return inputMedicalCharges; }
            set { inputMedicalCharges = value; notifyChange(); }
        }

        private decimal? inputSurgicalCharges;
        public decimal? InputSurgicalCharges
        {
            get { return inputSurgicalCharges; }
            set { inputSurgicalCharges = value; notifyChange(); }
        }

        private decimal? inputLabFees;
        public decimal? InputLabFees
        {
            get { return inputLabFees; }
            set { inputLabFees = value; notifyChange(); }
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
            return InputDays > 0 ? CHARGE_PER_DAY * (decimal)InputDays : 0;
        }

        private decimal CalcMiscCharge()
        {
            decimal? miscCharge = ((InputMedicalCharges ?? 0) + (InputSurgicalCharges ?? 0) + (InputLabFees ?? 0) + (InputRehabilitationCharges ?? 0));
            return miscCharge ?? 0;
        }

        private decimal CalcTotalCharges()
        {
            return CalcStayCharge(InputDays > 0 ? (int)InputDays : 0, CHARGE_PER_DAY) + CalcMiscCharge();
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
            DaysCharge = CalcStayCharge(InputDays > 0 ? (int)InputDays : 0, CHARGE_PER_DAY);
            MiscCharge = CalcMiscCharge();
            decimal total = CalcTotalCharges();
            TotalTax = CalcTax(total);
            TotalWithTax = total + TotalTax;
        }

        public void changeTheme()
        {
            ITheme theme = _paletteHelper.GetTheme();
            IBaseTheme baseTheme = IsDarkMode ? new MaterialDesignDarkTheme() : (IBaseTheme)new MaterialDesignLightTheme();
            theme.SetBaseTheme(baseTheme);
            _paletteHelper.SetTheme(theme);
        }

        private decimal CalcTax(decimal amount)
        {
            return amount > 0 ? (amount * (decimal)TAX_PERCENT / 100) : 0;
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
