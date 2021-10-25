using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace CustomerMaintainer
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public SeriesCollection SeriesCollection { get; private set; } = new SeriesCollection();
        public DateTime SelectedDate { get; private set; }

        public const int MinimumCustomerAmount = 0;
        public const int MaximumCustomerAmount = 50;

        private static readonly List<DateTime> DatesFromThisWeek = GetDatesFromThisWeek();
        private static readonly List<DateTime> DatesFromLastWeek = GetDatesFromLastWeek();

        private const int AmountOfYearsAgo = 2;
        public static int[] Years = new int[AmountOfYearsAgo];

        private const string upDownInMonthGridNamePrefix = "UpDownDay";


        public MainWindow()
        {
            InitializeComponent();
            UpdateYears();
        }

        private void UpdateYears()
        {
            for (int amountOfyears = 0; amountOfyears < AmountOfYearsAgo; amountOfyears++)
                Years[amountOfyears] = DateTime.Now.Year - amountOfyears;
        }

        private void TabSelected(object sender, SelectionChangedEventArgs e)
        {
            if (((TabControl)sender).SelectedIndex == 0)
            {
                UpdateYears();
                UpdateDatesOnLabels();
                LoadCustomersFromThisWeek();
                LoadCustomersFromLastWeek();
                ResetSpecificDatePicker();
            }
            else if (((TabControl)sender).SelectedIndex == 1)
            {
                if (MonthPicker.Visibility == Visibility.Hidden && MonthGrid.Visibility == Visibility.Visible && MonthPicker.DisplayDate != null)
                    UpdateMonthEditTab();
            }
            else if (((TabControl)sender).SelectedIndex == 2)
            {
                UpdateYears();
                UpdateCustomerAmountsFromThisYear();
                UpdateCustomerAmountsFromLastYear();
                CalculateDifferences();
                RemoveRedMarkingsOnMonthsStillToCome();
            }
            else if (((TabControl)sender).SelectedIndex == 3)
            {
                UpdateYears();
                DrawGraph();
            }
        }

        private void RemoveRedMarkingsOnMonthsStillToCome()
        {
            if (DifferenceFebLabel.Content.Equals((int)TotalPreviousFebLabel.Content * -1))
                DifferenceFebLabel.Foreground = new SolidColorBrush(Colors.Orange);
            if (DifferenceMarLabel.Content.Equals((int)TotalPreviousMarLabel.Content * -1))
                DifferenceMarLabel.Foreground = new SolidColorBrush(Colors.Orange);
            if (DifferenceAprLabel.Content.Equals((int)TotalPreviousAprLabel.Content * -1))
                DifferenceAprLabel.Foreground = new SolidColorBrush(Colors.Orange);
            if (DifferenceMayLabel.Content.Equals((int)TotalPreviousMayLabel.Content * -1))
                DifferenceMayLabel.Foreground = new SolidColorBrush(Colors.Orange);
            if (DifferenceJunLabel.Content.Equals((int)TotalPreviousJunLabel.Content * -1))
                DifferenceJunLabel.Foreground = new SolidColorBrush(Colors.Orange);
            if (DifferenceJulLabel.Content.Equals((int)TotalPreviousJulLabel.Content * -1))
                DifferenceJulLabel.Foreground = new SolidColorBrush(Colors.Orange);
            if (DifferenceAugLabel.Content.Equals((int)TotalPreviousAugLabel.Content * -1))
                DifferenceAugLabel.Foreground = new SolidColorBrush(Colors.Orange);
            if (DifferenceSepLabel.Content.Equals((int)TotalPreviousSepLabel.Content * -1))
                DifferenceSepLabel.Foreground = new SolidColorBrush(Colors.Orange);
            if (DifferenceOctLabel.Content.Equals((int)TotalPreviousOctLabel.Content * -1))
                DifferenceOctLabel.Foreground = new SolidColorBrush(Colors.Orange);
            if (DifferenceNovLabel.Content.Equals((int)TotalPreviousNovLabel.Content * -1))
                DifferenceNovLabel.Foreground = new SolidColorBrush(Colors.Orange);
            if (DifferenceDecLabel.Content.Equals((int)TotalPreviousDecLabel.Content * -1))
                DifferenceDecLabel.Foreground = new SolidColorBrush(Colors.Orange);
        }

        #region InputTab
        private void UpdateDatesOnLabels()
        {
            MondayLastWeekLabel.Content = DatesFromLastWeek[0].Day + "e";
            TuesdayLastWeekLabel.Content = DatesFromLastWeek[1].Day + "e";
            WednesdayLastWeekLabel.Content = DatesFromLastWeek[2].Day + "e";
            ThursdayLastWeekLabel.Content = DatesFromLastWeek[3].Day + "e";
            FridayLastWeekLabel.Content = DatesFromLastWeek[4].Day + "e";

            MondayThisWeekLabel.Content = DatesFromThisWeek[0].Day + "e";
            TuesdayThisWeekLabel.Content = DatesFromThisWeek[1].Day + "e";
            WednesdayThisWeekLabel.Content = DatesFromThisWeek[2].Day + "e";
            ThursdayThisWeekLabel.Content = DatesFromThisWeek[3].Day + "e";
            FridayThisWeekLabel.Content = DatesFromThisWeek[4].Day + "e";
        }

        private static List<DateTime> GetDatesFromThisWeek()
        {
            int currentDayOfWeek = (int)DateTime.Today.DayOfWeek;

            DateTime sunday = DateTime.Today.AddDays(-currentDayOfWeek);
            DateTime monday = sunday.AddDays(1);

            if (DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
                monday = monday.AddDays(-7);

            return Enumerable.Range(0, 5).Select(days => monday.AddDays(days)).ToList();
        }

        private static List<DateTime> GetDatesFromLastWeek()
        {
            int currentDayOfWeek = (int)DateTime.Today.DayOfWeek;

            DateTime sunday = DateTime.Today.AddDays(-currentDayOfWeek - 7);
            DateTime monday = sunday.AddDays(1);

            if (DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
                monday = monday.AddDays(-7);

            return Enumerable.Range(0, 5).Select(days => monday.AddDays(days)).ToList();
        }

        private void LoadCustomersFromThisWeek()
        {
            MondayThisWeekIntegerUpDown.Value = CustomerService.ReadAmountOfCustomersAtDate(DatesFromThisWeek[0]);
            TuesdayThisWeekIntegerUpDown.Value = CustomerService.ReadAmountOfCustomersAtDate(DatesFromThisWeek[1]);
            WednesdayThisWeekIntegerUpDown.Value = CustomerService.ReadAmountOfCustomersAtDate(DatesFromThisWeek[2]);
            ThursdayThisWeekIntegerUpDown.Value = CustomerService.ReadAmountOfCustomersAtDate(DatesFromThisWeek[3]);
            FridayThisWeekIntegerUpDown.Value = CustomerService.ReadAmountOfCustomersAtDate(DatesFromThisWeek[4]);
        }

        private void LoadCustomersFromLastWeek()
        {
            MondayLastWeekIntegerUpDown.Value = CustomerService.ReadAmountOfCustomersAtDate(DatesFromLastWeek[0]);
            TuesdayLastWeekIntegerUpDown.Value = CustomerService.ReadAmountOfCustomersAtDate(DatesFromLastWeek[1]);
            WednesdayLastWeekIntegerUpDown.Value = CustomerService.ReadAmountOfCustomersAtDate(DatesFromLastWeek[2]);
            ThursdayLastWeekIntegerUpDown.Value = CustomerService.ReadAmountOfCustomersAtDate(DatesFromLastWeek[3]);
            FridayLastWeekIntegerUpDown.Value = CustomerService.ReadAmountOfCustomersAtDate(DatesFromLastWeek[4]);
        }

        private void ResetSpecificDatePicker()
        {
            DatePicker.SelectedDate = null;
            SpecificDateUpDown.Visibility = Visibility.Hidden;
        }

        private void UpdateTotalAmountOfCustomersThisWeek()
        {
            TotalAmountOfCustomersThisWeek = MondayThisWeekIntegerUpDown.Value.GetValueOrDefault() +
                                             TuesdayThisWeekIntegerUpDown.Value.GetValueOrDefault() +
                                             WednesdayThisWeekIntegerUpDown.Value.GetValueOrDefault() +
                                             ThursdayThisWeekIntegerUpDown.Value.GetValueOrDefault() +
                                             FridayThisWeekIntegerUpDown.Value.GetValueOrDefault();
        }

        private void UpdateTotalAmountOfCustomersLastWeek()
        {
            TotalAmountOfCustomersLastWeek = MondayLastWeekIntegerUpDown.Value.GetValueOrDefault() +
                                             TuesdayLastWeekIntegerUpDown.Value.GetValueOrDefault() +
                                             WednesdayLastWeekIntegerUpDown.Value.GetValueOrDefault() +
                                             ThursdayLastWeekIntegerUpDown.Value.GetValueOrDefault() +
                                             FridayLastWeekIntegerUpDown.Value.GetValueOrDefault();
        }

        private void DateSelected(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = (DatePicker)sender;

            if (datePicker.SelectedDate != null)
            {
                DateTime selectedDate = datePicker.SelectedDate.Value;
                if (selectedDate.DayOfWeek == DayOfWeek.Saturday || selectedDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    MessageLabel.Content = "Je hebt een zaterdag of zondag geselecteerd.";
                    MessageLabel.Visibility = Visibility.Visible;
                    SpecificDateUpDown.Visibility = Visibility.Hidden;
                }
                else if (DatesFromThisWeek.Contains(selectedDate) || DatesFromLastWeek.Contains(selectedDate))
                {
                    MessageLabel.Content = "Je hebt een datum geselecteerd die al in te vullen is.";
                    MessageLabel.Visibility = Visibility.Visible;
                    SpecificDateUpDown.Visibility = Visibility.Hidden;
                    datePicker.SelectedDate = null;
                }
                else
                {
                    MessageLabel.Visibility = Visibility.Hidden;
                    SpecificDateUpDown.Visibility = Visibility.Visible;
                    SpecificDateUpDown.Value = CustomerService.ReadAmountOfCustomersAtDate(selectedDate);
                }
            }
        }

        private void AmountOfCustomersAtDateChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (DatePicker.SelectedDate != null)
            {
                IntegerUpDown upDown = (IntegerUpDown)sender;
                CustomerService.SaveAmountOfCustomersAtDate(DatePicker.SelectedDate.Value, Convert.ToInt32(upDown.Value));

                if (DatePicker.SelectedDate.Value.Year == DateTime.Now.Year)
                {
                    if (DatesFromLastWeek.Contains(DatePicker.SelectedDate.Value))
                        UpdateTotalAmountOfCustomersLastWeek();
                    else if (DatesFromThisWeek.Contains(DatePicker.SelectedDate.Value))
                        UpdateTotalAmountOfCustomersThisWeek();
                }
            }
        }

        private void AmountOfCustomersChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            IntegerUpDown upDown = (IntegerUpDown)sender;

            switch (upDown.Name)
            {
                case "MondayThisWeekIntegerUpDown":
                    CustomerService.SaveAmountOfCustomersAtDate(DatesFromThisWeek[0], upDown.Value);
                    UpdateTotalAmountOfCustomersThisWeek();
                    break;
                case "TuesdayThisWeekIntegerUpDown":
                    CustomerService.SaveAmountOfCustomersAtDate(DatesFromThisWeek[1], upDown.Value);
                    UpdateTotalAmountOfCustomersThisWeek();
                    break;
                case "WednesdayThisWeekIntegerUpDown":
                    CustomerService.SaveAmountOfCustomersAtDate(DatesFromThisWeek[2], upDown.Value);
                    UpdateTotalAmountOfCustomersThisWeek();
                    break;
                case "ThursdayThisWeekIntegerUpDown":
                    CustomerService.SaveAmountOfCustomersAtDate(DatesFromThisWeek[3], upDown.Value);
                    UpdateTotalAmountOfCustomersThisWeek();
                    break;
                case "FridayThisWeekIntegerUpDown":
                    CustomerService.SaveAmountOfCustomersAtDate(DatesFromThisWeek[4], upDown.Value);
                    UpdateTotalAmountOfCustomersThisWeek();
                    break;
                case "MondayLastWeekIntegerUpDown":
                    CustomerService.SaveAmountOfCustomersAtDate(DatesFromLastWeek[0], upDown.Value);
                    UpdateTotalAmountOfCustomersLastWeek();
                    break;
                case "TuesdayLastWeekIntegerUpDown":
                    CustomerService.SaveAmountOfCustomersAtDate(DatesFromLastWeek[1], upDown.Value);
                    UpdateTotalAmountOfCustomersLastWeek();
                    break;
                case "WednesdayLastWeekIntegerUpDown":
                    CustomerService.SaveAmountOfCustomersAtDate(DatesFromLastWeek[2], upDown.Value);
                    UpdateTotalAmountOfCustomersLastWeek();
                    break;
                case "ThursdayLastWeekIntegerUpDown":
                    CustomerService.SaveAmountOfCustomersAtDate(DatesFromLastWeek[3], upDown.Value);
                    UpdateTotalAmountOfCustomersLastWeek();
                    break;
                case "FridayLastWeekIntegerUpDown":
                    CustomerService.SaveAmountOfCustomersAtDate(DatesFromLastWeek[4], upDown.Value);
                    UpdateTotalAmountOfCustomersLastWeek();
                    break;
            }
        }
        #endregion

        #region EditMonthTab
        private void UpdateMonthEditTab()
        {
            RemoveStackPanelsFromMonthGrid();
            FillMonthGrid(MonthPicker.DisplayDate);
        }

        private void DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            if (MonthPicker.DisplayMode == CalendarMode.Month)
            {
                SelectedDate = MonthPicker.DisplayDate;
                MonthInYearLabel.Content = SelectedDate.ToString("MMMM") + " " + SelectedDate.Year;
                MonthPicker.DisplayMode = CalendarMode.Year;

                ToggleMonthPickerAndGridVisibility();

                FillMonthGrid(SelectedDate);
            }
        }

        private void ToggleMonthPickerAndGridVisibility()
        {
            MonthPicker.Visibility = (MonthPicker.Visibility == Visibility.Hidden) ? MonthPicker.Visibility = Visibility.Visible : MonthPicker.Visibility = Visibility.Hidden;
            MonthGrid.Visibility = (MonthGrid.Visibility == Visibility.Hidden) ? MonthGrid.Visibility = Visibility.Visible : MonthGrid.Visibility = Visibility.Hidden;
        }

        private void FillMonthGrid(DateTime SelectedDate)
        {
            DayOfWeek dayOfWeek = new DateTime(SelectedDate.Year, SelectedDate.Month, 1).DayOfWeek;
            int amountOfDaysInSelectedMonth = DateTime.DaysInMonth(SelectedDate.Year, SelectedDate.Month);
            int weekNumber = 1;

            for (int day = 1; day <= amountOfDaysInSelectedMonth; day++)
            {
                if (dayOfWeek != DayOfWeek.Saturday && dayOfWeek != DayOfWeek.Sunday)
                {
                    Label label = new Label
                    {
                        Content = day + "e"
                    };

                    IntegerUpDown upDown = new IntegerUpDown
                    {
                        Name = upDownInMonthGridNamePrefix + day,
                        Minimum = MinimumCustomerAmount,
                        Maximum = MaximumCustomerAmount,
                        Value = CustomerService.ReadAmountOfCustomersAtDate(new DateTime(SelectedDate.Year, SelectedDate.Month, day))
                    };
                    upDown.ValueChanged += AmountOfCustomersAtDateFromMonthChanged;

                    StackPanel panel = new StackPanel
                    {
                        Orientation = Orientation.Horizontal,
                        HorizontalAlignment = HorizontalAlignment.Right
                    };
                    panel.Children.Add(label);
                    panel.Children.Add(upDown);

                    Grid.SetColumn(panel, (int)dayOfWeek - 1);
                    Grid.SetRow(panel, 1 + weekNumber);
                    MonthGrid.Children.Add(panel);
                }

                if (dayOfWeek == DayOfWeek.Saturday)
                {
                    dayOfWeek = 0;
                    weekNumber++;
                }
                else
                    dayOfWeek++;
            }
        }

        private void AmountOfCustomersAtDateFromMonthChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            IntegerUpDown upDown = (IntegerUpDown)sender;

            if (SelectedDate != null)
            {
                DateTime selectedDate = new DateTime(SelectedDate.Year, SelectedDate.Month, GetDayFromUpDownName(upDown.Name));
                CustomerService.SaveAmountOfCustomersAtDate(selectedDate, Convert.ToInt32(upDown.Value));
            }
        }

        private int GetDayFromUpDownName(string name)
        {
            return Convert.ToInt32(name.Replace(upDownInMonthGridNamePrefix, ""));
        }

        private void SelectDifferentMonth(object sender, RoutedEventArgs e)
        {
            ToggleMonthPickerAndGridVisibility();
            RemoveStackPanelsFromMonthGrid();
        }

        private void RemoveStackPanelsFromMonthGrid()
        {
            List<UIElement> stackPanelsToRemove = new List<UIElement>();

            foreach (UIElement monthGridChild in MonthGrid.Children)
                if (monthGridChild is StackPanel)
                    stackPanelsToRemove.Add(monthGridChild);
            foreach (UIElement stackPanel in stackPanelsToRemove.ToArray())
                MonthGrid.Children.Remove(stackPanel);
        }
        #endregion

        #region OverviewTab
        private void UpdateCustomerAmountsFromThisYear()
        {
            const int thisYearIndex = 0;
            CurrentYearLabel.Content = Years[thisYearIndex];
            int[] amountOfCustomersInEachMonth = CustomerService.GetAmountOfCustomersInEachMonthFromYear(Years[thisYearIndex]);
            TotalCustomersJan = amountOfCustomersInEachMonth[0];
            TotalCustomersFeb = amountOfCustomersInEachMonth[1];
            TotalCustomersMar = amountOfCustomersInEachMonth[2];
            TotalCustomersApr = amountOfCustomersInEachMonth[3];
            TotalCustomersMay = amountOfCustomersInEachMonth[4];
            TotalCustomersJun = amountOfCustomersInEachMonth[5];
            TotalCustomersJul = amountOfCustomersInEachMonth[6];
            TotalCustomersAug = amountOfCustomersInEachMonth[7];
            TotalCustomersSep = amountOfCustomersInEachMonth[8];
            TotalCustomersOct = amountOfCustomersInEachMonth[9];
            TotalCustomersNov = amountOfCustomersInEachMonth[10];
            TotalCustomersDec = amountOfCustomersInEachMonth[11];
            TotalAmountOfCustomersThisYear = amountOfCustomersInEachMonth.Sum();
        }

        private void UpdateCustomerAmountsFromLastYear()
        {
            const int lastYearIndex = 1;
            LastYearLabel.Content = Years[lastYearIndex];
            int[] amountOfCustomersInEachMonth = CustomerService.GetAmountOfCustomersInEachMonthFromYear(Years[lastYearIndex]);
            TotalCustomersPreviousJan = amountOfCustomersInEachMonth[0];
            TotalCustomersPreviousFeb = amountOfCustomersInEachMonth[1];
            TotalCustomersPreviousMar = amountOfCustomersInEachMonth[2];
            TotalCustomersPreviousApr = amountOfCustomersInEachMonth[3];
            TotalCustomersPreviousMay = amountOfCustomersInEachMonth[4];
            TotalCustomersPreviousJun = amountOfCustomersInEachMonth[5];
            TotalCustomersPreviousJul = amountOfCustomersInEachMonth[6];
            TotalCustomersPreviousAug = amountOfCustomersInEachMonth[7];
            TotalCustomersPreviousSep = amountOfCustomersInEachMonth[8];
            TotalCustomersPreviousOct = amountOfCustomersInEachMonth[9];
            TotalCustomersPreviousNov = amountOfCustomersInEachMonth[10];
            TotalCustomersPreviousDec = amountOfCustomersInEachMonth[11];
            TotalAmountOfCustomersLastYear = amountOfCustomersInEachMonth.Sum();
        }

        private void CalculateDifferences()
        {
            DifferenceJan = TotalCustomersJan - TotalCustomersPreviousJan;
            DifferenceFeb = TotalCustomersFeb - TotalCustomersPreviousFeb;
            DifferenceMar = TotalCustomersMar - TotalCustomersPreviousMar;
            DifferenceApr = TotalCustomersApr - TotalCustomersPreviousApr;
            DifferenceMay = TotalCustomersMay - TotalCustomersPreviousMay;
            DifferenceJun = TotalCustomersJun - TotalCustomersPreviousJun;
            DifferenceJul = TotalCustomersJul - TotalCustomersPreviousJul;
            DifferenceAug = TotalCustomersAug - TotalCustomersPreviousAug;
            DifferenceSep = TotalCustomersSep - TotalCustomersPreviousSep;
            DifferenceOct = TotalCustomersOct - TotalCustomersPreviousOct;
            DifferenceNov = TotalCustomersNov - TotalCustomersPreviousNov;
            DifferenceDec = TotalCustomersDec - TotalCustomersPreviousDec;
            TotalDifference = TotalAmountOfCustomersThisYear - TotalAmountOfCustomersLastYear;
            TotalDifferenceComparedToDate = CustomerService.GetTotalDifferenceComparedToDate(Years);

            double averageToGo = CustomerService.GetAverageToGoPerDayThisMonth(Years);
            if (averageToGo > 0)
            {
                AveragePerDayToGoThisMonth = averageToGo;
                AverageToGoLabel.Background = new SolidColorBrush(Colors.White);
                AverageToGoAmountLabel.Visibility = Visibility.Visible;
            }
            else
            {
                AverageToGoAmountLabel.Visibility = Visibility.Hidden;
                AverageToGoLabel.Background = new SolidColorBrush(Colors.Green);
            }
        }
        #endregion

        #region GraphTab
        private void DrawGraph()
        {
            if (SeriesCollection.Count > 0)
                SeriesCollection = new SeriesCollection();

            for (int index = 0; index < Years.Length; index++)
            {
                SeriesCollection.Add(new LineSeries
                {
                    PointGeometrySize = 7,
                    LineSmoothness = 1,
                    Title = Years[index].ToString(),
                    Values = GetMonthValuesFromYear(Years[index])
                });
            }

            if (LineChart.DataContext != null)
                LineChart.DataContext = null;
            LineChart.DataContext = this;
        }

        private ChartValues<double> GetMonthValuesFromYear(int year)
        {
            int[] amountOfCustomersInEachMonth = CustomerService.GetAmountOfCustomersInEachMonthFromYear(year);
            ChartValues<double> monthValues = new ChartValues<double>();

            for (int monthNumber = 0; monthNumber <= 11; monthNumber++)
                monthValues.Add(amountOfCustomersInEachMonth[monthNumber]);
            return monthValues;
        }
        #endregion

        #region Properties
        private double _averagePerDayToGoThisMonth;
        public double AveragePerDayToGoThisMonth
        {
            get => _averagePerDayToGoThisMonth;
            set { _averagePerDayToGoThisMonth = value; OnPropertyChanged(); }
        }

        private int _totalDifferenceComparedToDate;
        public int TotalDifferenceComparedToDate
        {
            get => _totalDifferenceComparedToDate;
            set { _totalDifferenceComparedToDate = value; OnPropertyChanged(); }
        }

        private int _totalDifference;
        public int TotalDifference
        {
            get => _totalDifference;
            set { _totalDifference = value; OnPropertyChanged(); }
        }

        private int _totalAmountOfCustomersThisYear;
        public int TotalAmountOfCustomersThisYear
        {
            get => _totalAmountOfCustomersThisYear;
            set { _totalAmountOfCustomersThisYear = value; OnPropertyChanged(); }
        }

        private int _totalAmountOfCustomersLastYear;
        public int TotalAmountOfCustomersLastYear
        {
            get => _totalAmountOfCustomersLastYear;
            set { _totalAmountOfCustomersLastYear = value; OnPropertyChanged(); }
        }

        private int _totalAmountOfCustomersThisWeek;
        public int TotalAmountOfCustomersThisWeek
        {
            get => _totalAmountOfCustomersThisWeek;
            set { _totalAmountOfCustomersThisWeek = value; OnPropertyChanged(); }
        }

        private int _totalAmountOfCustomersLastWeek;
        public int TotalAmountOfCustomersLastWeek
        {
            get => _totalAmountOfCustomersLastWeek;
            set { _totalAmountOfCustomersLastWeek = value; OnPropertyChanged(); }
        }

        private int _totalCustomersJan;
        public int TotalCustomersJan
        {
            get => _totalCustomersJan;
            set { _totalCustomersJan = value; OnPropertyChanged(); }
        }

        private int _totalCustomersFeb;
        public int TotalCustomersFeb
        {
            get => _totalCustomersFeb;
            set { _totalCustomersFeb = value; OnPropertyChanged(); }
        }

        private int _totalCustomersMar;
        public int TotalCustomersMar
        {
            get => _totalCustomersMar;
            set { _totalCustomersMar = value; OnPropertyChanged(); }
        }

        private int _totalCustomersApr;
        public int TotalCustomersApr
        {
            get => _totalCustomersApr;
            set { _totalCustomersApr = value; OnPropertyChanged(); }
        }

        private int _totalCustomersMay;
        public int TotalCustomersMay
        {
            get => _totalCustomersMay;
            set { _totalCustomersMay = value; OnPropertyChanged(); }
        }

        private int _totalCustomersJun;
        public int TotalCustomersJun
        {
            get => _totalCustomersJun;
            set { _totalCustomersJun = value; OnPropertyChanged(); }
        }

        private int _totalCustomersJul;
        public int TotalCustomersJul
        {
            get => _totalCustomersJul;
            set { _totalCustomersJul = value; OnPropertyChanged(); }
        }

        private int _totalCustomersAug;
        public int TotalCustomersAug
        {
            get => _totalCustomersAug;
            set { _totalCustomersAug = value; OnPropertyChanged(); }
        }

        private int _totalCustomersSep;
        public int TotalCustomersSep
        {
            get => _totalCustomersSep;
            set { _totalCustomersSep = value; OnPropertyChanged(); }
        }

        private int _totalCustomersOct;
        public int TotalCustomersOct
        {
            get => _totalCustomersOct;
            set { _totalCustomersOct = value; OnPropertyChanged(); }
        }

        private int _totalCustomersNov;
        public int TotalCustomersNov
        {
            get => _totalCustomersNov;
            set { _totalCustomersNov = value; OnPropertyChanged(); }
        }

        private int _totalCustomersDec;
        public int TotalCustomersDec
        {
            get => _totalCustomersDec;
            set { _totalCustomersDec = value; OnPropertyChanged(); }
        }

        private int _totalCustomersPreviousJan;
        public int TotalCustomersPreviousJan
        {
            get => _totalCustomersPreviousJan;
            set { _totalCustomersPreviousJan = value; OnPropertyChanged(); }
        }

        private int _totalCustomersPreviousFeb;
        public int TotalCustomersPreviousFeb
        {
            get => _totalCustomersPreviousFeb;
            set { _totalCustomersPreviousFeb = value; OnPropertyChanged(); }
        }

        private int _totalCustomersPreviousMar;
        public int TotalCustomersPreviousMar
        {
            get => _totalCustomersPreviousMar;
            set { _totalCustomersPreviousMar = value; OnPropertyChanged(); }
        }

        private int _totalCustomersPreviousApr;
        public int TotalCustomersPreviousApr
        {
            get => _totalCustomersPreviousApr;
            set { _totalCustomersPreviousApr = value; OnPropertyChanged(); }
        }

        private int _totalCustomersPreviousMay;
        public int TotalCustomersPreviousMay
        {
            get => _totalCustomersPreviousMay;
            set { _totalCustomersPreviousMay = value; OnPropertyChanged(); }
        }

        private int _totalCustomersPreviousJun;
        public int TotalCustomersPreviousJun
        {
            get => _totalCustomersPreviousJun;
            set { _totalCustomersPreviousJun = value; OnPropertyChanged(); }
        }

        private int _totalCustomersPreviousJul;
        public int TotalCustomersPreviousJul
        {
            get => _totalCustomersPreviousJul;
            set { _totalCustomersPreviousJul = value; OnPropertyChanged(); }
        }

        private int _totalCustomersPreviousAug;
        public int TotalCustomersPreviousAug
        {
            get => _totalCustomersPreviousAug;
            set { _totalCustomersPreviousAug = value; OnPropertyChanged(); }
        }

        private int _totalCustomersPreviousSep;
        public int TotalCustomersPreviousSep
        {
            get => _totalCustomersPreviousSep;
            set { _totalCustomersPreviousSep = value; OnPropertyChanged(); }
        }

        private int _totalCustomersPreviousOct;
        public int TotalCustomersPreviousOct
        {
            get => _totalCustomersPreviousOct;
            set { _totalCustomersPreviousOct = value; OnPropertyChanged(); }
        }

        private int _totalCustomersPreviousNov;
        public int TotalCustomersPreviousNov
        {
            get => _totalCustomersPreviousNov;
            set { _totalCustomersPreviousNov = value; OnPropertyChanged(); }
        }

        private int _totalCustomersPreviousDec;
        public int TotalCustomersPreviousDec
        {
            get => _totalCustomersPreviousDec;
            set { _totalCustomersPreviousDec = value; OnPropertyChanged(); }
        }

        private int _differenceJan;
        public int DifferenceJan
        {
            get => _differenceJan;
            set { _differenceJan = value; OnPropertyChanged(); }
        }

        private int _differenceFeb;
        public int DifferenceFeb
        {
            get => _differenceFeb;
            set { _differenceFeb = value; OnPropertyChanged(); }
        }

        private int _differenceMar;
        public int DifferenceMar
        {
            get => _differenceMar;
            set { _differenceMar = value; OnPropertyChanged(); }
        }

        private int _differenceApr;
        public int DifferenceApr
        {
            get => _differenceApr;
            set { _differenceApr = value; OnPropertyChanged(); }
        }

        private int _differenceMay;
        public int DifferenceMay
        {
            get => _differenceMay;
            set { _differenceMay = value; OnPropertyChanged(); }
        }

        private int _differenceJun;
        public int DifferenceJun
        {
            get => _differenceJun;
            set { _differenceJun = value; OnPropertyChanged(); }
        }

        private int _differenceJul;
        public int DifferenceJul
        {
            get => _differenceJul;
            set { _differenceJul = value; OnPropertyChanged(); }
        }

        private int _differenceAug;
        public int DifferenceAug
        {
            get => _differenceAug;
            set { _differenceAug = value; OnPropertyChanged(); }
        }

        private int _differenceSep;
        public int DifferenceSep
        {
            get => _differenceSep;
            set { _differenceSep = value; OnPropertyChanged(); }
        }

        private int _differenceOct;
        public int DifferenceOct
        {
            get => _differenceOct;
            set { _differenceOct = value; OnPropertyChanged(); }
        }

        private int _differenceNov;
        public int DifferenceNov
        {
            get => _differenceNov;
            set { _differenceNov = value; OnPropertyChanged(); }
        }

        private int _differenceDec;
        public int DifferenceDec
        {
            get => _differenceDec;
            set { _differenceDec = value; OnPropertyChanged(); }
        }
        #endregion

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
