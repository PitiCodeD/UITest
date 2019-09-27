using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace UITest.ViewModel
{
    public class CalculateViewModel : INotifyPropertyChanged
    {
        public CalculateViewModel()
        {
            Plus = new Command(PlusMethod);
            Minus = new Command(MinusMethod);
            Multiply = new Command(MultiplyMethod);
            Divide = new Command(DivideMethod);
        }

        private int dNum1;
        private string num1;
        public string Num1
        {

            get
            {
                return num1;
            }
            set
            {
                num1 = CheckInputDigit(value, dNum1);
                dNum1 = CheckOutputDigit(num1, dNum1);
                OnPropertyChanged();
            }
        }

        private double fNum2;
        private string num2;
        public string Num2
        {
            get
            {
                return num2;
            }
            set
            {
                num2 = CheckInputDigit(value, fNum2);
                fNum2 = CheckOutputDigit(num2, fNum2);
                OnPropertyChanged();
            }
        }

        private double result;
        public double Result
        {
            get
            {
                return result;
            }
            set
            {
                result = value;
                OnPropertyChanged();
            }
        }

        public string CheckInputDigit(string input, int output)
        {
            if (!CheckConventToInt32(input) && !String.IsNullOrEmpty(input))
            {
                input = input.Remove(input.Length - 1);
            }
            return input;
        }

        public string CheckInputDigit(string input, double output)
        {
            if ((!CheckIntegerAndPoint(input) || (!CheckConventToDouble(input) && !CheckIntegerLastPoint(input))) 
                && !String.IsNullOrEmpty(input))
            {
                input = input.Remove(input.Length - 1);
            }
            return input;
        }

        public int CheckOutputDigit(string input, int output)
        {

            if (!CheckConventToInt32(input))
            {
                return 0;
            }
            return Int32.Parse(input);
        }

        public double CheckOutputDigit(string input, double output)
        {
            if (!CheckConventToDouble(input))
            {
                return 0;
            }
            return Double.Parse(input);
        }

        public bool CheckInteger(string digit)
        {
            if (String.IsNullOrEmpty(digit))
            {
                return false;
            }
            string pattern = @"^(\d)+$";
            return Regex.IsMatch(digit, pattern);
        }

        public bool CheckIntegerAndPoint(string digit)
        {
            if (String.IsNullOrEmpty(digit))
            {
                return false;
            }
            string pattern = @"^((\d)+([.]){0,1}(\d)*)$";
            return Regex.IsMatch(digit, pattern);
        }

        public bool CheckIntegerLastPoint(string digit)
        {
            if (String.IsNullOrEmpty(digit))
            {
                return false;
            }
            string pattern = @"^((\d)+([.]){1})$";
            return Regex.IsMatch(digit, pattern);
        }

        public bool CheckDecimal(string digit)
        {
            if (String.IsNullOrEmpty(digit))
            {
                return false;
            }
            string pattern = @"^((\d)+(([.]){0,1}(\d)+)*)$";
            return Regex.IsMatch(digit, pattern);
        }

        public string DelPointMoreThen1(string value)
        {
            int pointIndex = value.IndexOf('.', value.IndexOf('.') + 1);
            if (pointIndex >= 0 && pointIndex <= value.Length)
            {
                value = value.Remove(pointIndex);
            }
            return value;
        }

        public bool CheckConventToInt32(string value)
        {
            try
            {
                int lack = Int32.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckConventToDouble(string value)
        {
            try
            {
                double lack = Double.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }




        public ICommand Plus { get; set; }
        public void PlusMethod()
        {
            if (CheckIntegerLastPoint(num2))
            {
                Application.Current.MainPage.DisplayAlert("Error!!!", "Invalid Input Value", "Ok");
            }
            else
            {
                Result = dNum1 + fNum2;
            }
            
        }

        public ICommand Minus { get; set; }
        public void MinusMethod()
        {
            if (CheckIntegerLastPoint(num2))
            {
                Application.Current.MainPage.DisplayAlert("Error!!!", "Invalid Input Value", "Ok");
            }
            else if (dNum1 < fNum2)
            {
                Application.Current.MainPage.DisplayAlert("Error!!!", "Num02 More Than Num01", "Ok");
            }
            else
            {
                Result = dNum1 - fNum2;
            }
        }

        public ICommand Multiply { get; set; }
        public void MultiplyMethod()
        {
            if (CheckIntegerLastPoint(num2))
            {
                Application.Current.MainPage.DisplayAlert("Error!!!", "Invalid Input Value", "Ok");
            }
            else
            {
                Result = dNum1 * fNum2;
            }
        }

        public ICommand Divide { get; set; }
        public void DivideMethod()
        {
            if (CheckIntegerLastPoint(num2))
            {
                Application.Current.MainPage.DisplayAlert("Error!!!", "Invalid Input Value", "Ok");
            }
            else if (fNum2 <= 0)
            {
                Application.Current.MainPage.DisplayAlert("Error!!!", "Can't divide by 0", "Ok");
            }
            else
            {
                Result = dNum1 / fNum2;
            }
        }

        public int TestPlus(int a, int b)
        {
            return a + b;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
