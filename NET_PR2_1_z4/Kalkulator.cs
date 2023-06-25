using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_PR2_1_z4
{
	class Kalkulator : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		private string?
			wynik = "0",
			operacja = null
			;
		private double?
			operandLewy = null,
			operandPrawy = null
			;

		public bool operacjaWykonana = false;


        public string Wynik {
			get => wynik;
			set {
				wynik = value;
				PropertyChanged?.Invoke(
					this,
					new PropertyChangedEventArgs("Wynik")
					);
			}
		}
		public string Działanie {
			get
			{
				if (operandLewy == null)
					return "";
				else if (operandPrawy == null)
					return $"{operandLewy} {operacja}";
				else if (operacja == "x^2")
				{
                    double liczba = double.Parse(operandLewy.ToString());
                    return $"sqrt({liczba})";
                }
                else if (operacja == "1/x")
                {
                    double liczba = double.Parse(operandLewy.ToString());
                    return $"1/({liczba})";
                }
                else if (operacja == "n!")
                {
                    double liczba = double.Parse(operandLewy.ToString());
                    return $"silnia({liczba})";
                }
                else
					return $"{operandLewy} {operacja} {operandPrawy} = ";
			}
		}

		internal void WprowadźCyfrę(string? cyfra)
		{
			if (operandLewy != null && operandPrawy != null && operacja != null)
			{
				CzyśćWszystko();
                Wynik = cyfra;
			}
			else { 
				if (wynik == "0")
					if (cyfra == "0")
						return;
					else
						Wynik = cyfra;
				else
					Wynik += cyfra;
            }
        }

		internal void WprowadźPrzecinek()
		{
			if (wynik.Contains(','))
				return;
			else
				Wynik += ',';
		}

		internal void ZmieńZnak()
		{
			if (wynik == "0")
				return;
			else if (wynik[0] == '-')
				Wynik = wynik.Substring(1);
			else
				Wynik = '-' + wynik;
		}

		internal void KasujZnak()
		{
			if (wynik == "0")
				return;
			else if (
				wynik.Length == 1
				|| (wynik.Length == 2 && wynik[0] == '-')
				|| wynik == "-0,"
				)
				Wynik = "0";
			else
				Wynik = wynik.Substring(0, wynik.Length - 1);
		}

		internal void CzyśćWszystko()
		{
			CzyśćWynik();
			operacja = null;
			operandLewy = operandPrawy = null;
			PropertyChanged?.Invoke(
				this,
				new PropertyChangedEventArgs("Działanie")
				);
		}

		internal void CzyśćWynik()
		{
			Wynik = "0";
		}

        internal void WprowadźOperacja(string operacja)
        {
            if (this.operacja != null)
            {
                if (operandLewy != null && operandPrawy == null && wynik != "0")
                {
                    WykonajDziałanie();
                    operacjaWykonana = true;
                }
                this.operacja = operacja;
                operandPrawy = null;
            }
            else
            {
                operandLewy = Convert.ToDouble(wynik);
                this.operacja = operacja;
            }

            wynik = "0";
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Działanie"));
        }

        internal void WykonajDziałanie()
		{
			if (operandPrawy == null)
			{
				if (wynik == "0")
					operandPrawy = operandLewy;
				else
					operandPrawy = Convert.ToDouble(wynik);
			}
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Działanie"));
			if (!operacjaWykonana)
			{
				// Dodawanie
				if (operacja == "+")
				{
					Wynik = (operandLewy + operandPrawy).ToString();
				}
				// Odejmowanie
				else if (operacja == "-")
				{
					Wynik = (operandLewy - operandPrawy).ToString();
				}
				// Mnożenie
				else if (operacja == "*")
				{
					Wynik = (operandLewy * operandPrawy).ToString();
				}
				// Dzielenie
				else if (operacja == "/")
				{
					if (operandPrawy == 0)
					{
						return;
					}
					else
					{
						Wynik = (operandLewy / operandPrawy).ToString();

					}
				}
				else if (operacja == "x^2")
				{
					Wynik = (operandLewy * operandLewy).ToString();
				}
				else if (operacja == "^")
				{
					double w = Math.Pow((double)operandLewy, (double)operandPrawy);
					Wynik = w.ToString();

				}
				else if (operacja == "mod")
				{
					if (operandPrawy == 0)
						return;
					else
					{
						Wynik = (operandLewy % operandPrawy).ToString();
					}
				}
				else if (operacja == "1/x")
				{
					if (operandLewy == 0)
					{
						Wynik = "Nie można dzielić przez zero";
						return;
					}
					else
					{
						Wynik = (1.0 / operandLewy).ToString();
					}
				}
				else if (operacja == "n!")
				{
                    double wynik = 1;
                    for (int i = 1; i <= (int)operandLewy; i++)
                    {
                        wynik *= i;
                    }
                    Wynik = wynik.ToString();
                }
                else if (operacja == "log")
                {
                    double w = Math.Log10((double)operandLewy);
                    Wynik = w.ToString();
                }
                else if (operacja == "up")
                {
                    int zaokraglone = (int)Math.Ceiling((double)operandLewy);
					Wynik = zaokraglone.ToString();
                }
                else if (operacja == "down")
                {
                    int zaokraglone = (int)Math.Floor((double)operandLewy);
                    Wynik = zaokraglone.ToString();
                }
            }
			else
			{
				return;
			}
            operandLewy = Convert.ToDouble(wynik);
        }
	}
}
