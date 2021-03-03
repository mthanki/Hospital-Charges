using MaterialDesignThemes.Wpf;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Hospital_Charges
{
    class VM : INotifyPropertyChanged
    {
        private readonly PaletteHelper _paletteHelper = new PaletteHelper();

        readonly StringBuilder output = new StringBuilder();
        string fullName;
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), RESULT_FOLDER_NAME);

        #region Constants
        const float TAX_PERCENT = 13;
        const decimal CHARGE_PER_DAY = 350;
        const string FILENAME = "reciept.txt";
        const string RESULT_FOLDER_NAME = "HospitalCharges";
        #endregion

        #region Properties
        private decimal daysCharge;
        public decimal DaysCharge
        {
            get { return daysCharge; }
            set { daysCharge = value; NotifyChange(); }
        }

        private bool isDarkMode = false;
        public bool IsDarkMode
        {
            get { return isDarkMode; }
            set { isDarkMode = value; NotifyChange(); }
        }

        private int? inputDays;
        public int? InputDays
        {
            get { return inputDays; }
            set { inputDays = value; NotifyChange(); }
        }

        private decimal daysChargeTax;
        public decimal DaysChargeTax
        {
            get { return daysChargeTax; }
            set { daysChargeTax = value; NotifyChange(); }
        }

        private decimal miscCharge;
        public decimal MiscCharge
        {
            get { return miscCharge; }
            set { miscCharge = value; NotifyChange(); }
        }

        private int taxPercent = 0;
        public int TaxPercent
        {
            get { return taxPercent; }
            set { taxPercent = value; NotifyChange(); }
        }

        private decimal totalTax = 0;
        public decimal TotalTax
        {
            get { return totalTax; }
            set { totalTax = value; NotifyChange(); }
        }

        private decimal totalWithTax = 0;
        public decimal TotalWithTax
        {
            get { return totalWithTax; }
            set { totalWithTax = value; NotifyChange(); }
        }

        private decimal? inputMedicalCharges;
        public decimal? InputMedicalCharges
        {
            get { return inputMedicalCharges; }
            set { inputMedicalCharges = value; NotifyChange(); }
        }

        private decimal? inputSurgicalCharges;
        public decimal? InputSurgicalCharges
        {
            get { return inputSurgicalCharges; }
            set { inputSurgicalCharges = value; NotifyChange(); }
        }

        private decimal? inputLabFees;
        public decimal? InputLabFees
        {
            get { return inputLabFees; }
            set { inputLabFees = value; NotifyChange(); }
        }

        private decimal? inputRehabilitationCharges;
        public decimal? InputRehabilitationCharges
        {
            get { return inputRehabilitationCharges; }
            set { inputRehabilitationCharges = value; NotifyChange(); }
        }
        #endregion

        #region Methods
        private decimal CalcStayCharge(int daysInput, decimal perDayCharge)
        {
            return InputDays > 0 ? CHARGE_PER_DAY * (decimal)InputDays : 0;
        }

        private decimal CalcMiscCharge()
        {
            decimal? miscCharge = (InputMedicalCharges ?? 0) + (InputSurgicalCharges ?? 0) + (InputLabFees ?? 0) + (InputRehabilitationCharges ?? 0);
            return miscCharge ?? 0;
        }

        private decimal CalcTotalCharges()
        {
            return CalcStayCharge(InputDays > 0 ? (int)InputDays : 0, CHARGE_PER_DAY) + CalcMiscCharge();
        }

        public void Calculate()
        {
            DaysCharge = CalcStayCharge(InputDays > 0 ? (int)InputDays : 0, CHARGE_PER_DAY);
            MiscCharge = CalcMiscCharge();
            decimal total = CalcTotalCharges();
            TotalTax = CalcTax(total);
            TotalWithTax = total + TotalTax;
        }

        public void CreateDirectory()
        {
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            fullName = Path.Combine(filePath, FILENAME);
        }

        public void GenerateRecipt()
        {
            output.Append($"Date: {DateTime.Now:MMMM dd, yyyy HH:mm}{Environment.NewLine}");
            output.Append($"Stay Charge: ${DaysCharge.ToString()}{Environment.NewLine}");
            output.Append($"Miscellaneous Charge: ${MiscCharge.ToString()}{Environment.NewLine}");
            output.Append($"Tax: ${TotalTax.ToString()}{Environment.NewLine}");
            output.Append($"Total: ${TotalWithTax.ToString()}{Environment.NewLine}");
            output.AppendLine();
            File.AppendAllText(fullName, output.ToString());
        }

        public void ChangeTheme()
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
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyChange([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
