﻿using System;
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
            miscCharge = miscCharge > 0 ? AddTax((decimal)miscCharge) : 0;

            return (decimal)miscCharge;
        }

        private decimal CalcTotalCharges()
        {
            return CalcStayCharge((int)InputDays, CHARGE_PER_DAY) + CalcMiscCharge();
        }

        public void AddChargesWithTax()
        {
            DaysCharge = 0;
            DaysCharge = AddTax(DaysCharge);
            MedicalCharges = AddTax((decimal)InputMedicalCharges);
            SurgicalCharges = AddTax((decimal)InputSurgicalCharges);
            LabFees = AddTax((decimal)InputLabFees);
            RehabilitationCharges = AddTax((decimal)InputRehabilitationCharges);

            Total = CalcTotalCharges();
        }

        public void AddChargesWithoutTax()
        {
            DaysCharge = 0;
            //DaysCharge = CalcStayCharge();
            MedicalCharges = (decimal)InputMedicalCharges;
            SurgicalCharges = (decimal)InputSurgicalCharges;
            LabFees = (decimal)InputLabFees;
            RehabilitationCharges = (decimal)InputRehabilitationCharges;

            Total = CalcTotalCharges();
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
                DaysCharge = CalcStayCharge((int)InputDays, CHARGE_PER_DAY);
                AddChargesWithoutTax();
            }
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
