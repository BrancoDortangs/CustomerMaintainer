   M S B A M L   `   `   `  ���� M  ICustomerMaintainer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:CustomerMaintainerCustomerMaintainer  U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf #    CustomerMaintainer.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     2local clr-namespace:CustomerMaintainer     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=3.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf    �Language$  nl-NL�-    �Name$ Window��$��1030.4�� � 	Resources 1   +CustomerMaintainer.DifferenceColorConverter   DifferenceColorConverter��%   &	          .��� .+��� -   .@��� $��Invoeren��$��Stretch=�.��� 
������� � ColumnDefinitions �� $	��100 ��� $	��100 ��� $	��100 ��� $	��200 ��� $
��100* � � RowDefinitions �� $��30 ��� $��30 ��� $��4 ��� $��30 ��� $��4 ��� $��30 ��� $��4 ��� $��30 ��� $��4 ��� $��30 ��� $��8 ��� $��30 ��� $��8 ��� $
��100* �.+��� $��Vorige week��$��0��$��1��$��Center=�$
��Bold"�$��14(��� $��maandag��$��1��$��0���� $��dinsdag��$��3��$��0���� $��woensdag��$��5��$��0���� $��	donderdag��$��7��$��0���� $��vrijdag��$��9��$��0��%  Xceed.Wpf.Toolkit.IntegerUpDown  -     Name$! MondayLastWeekIntegerUpDown��$��1��$��1���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Minimum$ 0X�   Maximum$ 50X�$
��20,0q�  -   $" TuesdayLastWeekIntegerUpDown��$��3��$��1��$ 0X�$ 50X�$
��20,0q�  -   $$ WednesdayLastWeekIntegerUpDown��$��5��$��1��$ 0X�$ 50X�$
��20,0q�  -   $# ThursdayLastWeekIntegerUpDown��$��7��$��1��$ 0X�$ 50X�$
��20,0q�  -   $! FridayLastWeekIntegerUpDown��$��9��$��1��$ 0X�$ 50X�$
��20,0q��� $��	Deze week��$��0��$��2��$��Center=�$
��Bold"�$��14(�  -   $! MondayThisWeekIntegerUpDown��$��1��$��2��$ 0X�$ 50X�$
��20,0q�  -	   $" TuesdayThisWeekIntegerUpDown��$��3��$��2��$ 0X�$ 50X�$
��20,0q�  -
   $$ WednesdayThisWeekIntegerUpDown��$��5��$��2��$ 0X�$ 50X�$
��20,0q�  -   $# ThursdayThisWeekIntegerUpDown��$��7��$��2��$ 0X�$ 50X�$
��20,0q�  -   $! FridayThisWeekIntegerUpDown��$��9��$��2��$ 0X�$ 50X�$
��20,0q��� $��Selecteer een dag��$��0��$��3��$��Center=�$
��Bold"�$��14(�(  "System.Windows.Controls.DatePicker  -     Name$ 
DatePicker��$��1��$��3��$��2��$��
10,10,10,0q�  -   $ SpecificDateUpDown��$��5��$��3��$
��60,2q�$ 0X�$ 50X�y��� *SpecificDateUpDownIsVisible+	 �� ElementName$	 Window���� -   
 ��Name$
 MessageLabel��$��5��$��3��$��2��$
��Left=�$
��Bold"�$��14(�
���  ��$y�Hidden=��� $��
Weektotaal��$��11��$��0��$��Center=�$��14(�$
��Bold"��� -   $
 TotalLastWeekLabel��$��11��$��1��$��Center=�$
��Bold"�$��14(����� * TotalAmountOfCustomersLastWeek+$	 Window���� -   $
 TotalThisWeekLabel��$��11��$��2��$��Center=�$
��Bold"�$��14(����� * TotalAmountOfCustomersThisWeek+$	 Window���� $��Maand bewerken��.��� .+�� $��20q�.+�&   System.Windows.Controls.Calendar  -     Name$ MonthPicker��$
��Left=�   SelectionMode$
 None=�   DisplayMode$
 Year=�� -    �Name$ 	MonthGrid��
�������$y�Hidden=� �� $	��100 ��� $	��100 ��� $	��100 ��� $	��100 ��� $	��100 ��� $
��100* � �� $��40 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 �.+��� -   $ ��SelectDifferentMonthButton��$��Andere maand��$��Right=�$	��Top=�$��	0,10,10,0q��� -   $��MonthInYearLabel��$��0��$��1��$��2��$��Center=�$��Center=�$
��Bold"�$��18(��� $��maandag��$��1��$��0��$��Center=��� $��dinsdag��$��1��$��1��$��Center=��� $��woensdag��$��1��$��2��$��Center=��� $��	donderdag��$��1��$��3��$��Center=��� $��vrijdag��$��1��$��4��$��Center=��� -   $��	Overzicht������ $��	Overzicht��.��� 
������� �� $	��100 ��� $	��100 ��� $	��100 ��� $	��100 � �� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 �.+��� $��1��$��0��$��Center=�$��14(�$��januari���� $��2��$��0��$��Center=�$��14(�$��februari���� $��3��$��0��$��Center=�$��14(�$��maart���� $��4��$��0��$��Center=�$��14(�$��april���� $��5��$��0��$��Center=�$��14(�$	��mei���� $��6��$��0��$��Center=�$��14(�$
��juni���� $��7��$��0��$��Center=�$��14(�$
��juli���� $��8��$��0��$��Center=�$��14(�$��augustus���� $��9��$��0��$��Center=�$��14(�$��	september���� $��10��$��0��$��Center=�$��14(�$��oktober���� $��11��$��0��$��Center=�$��14(�$��november���� $��12��$��0��$��Center=�$��14(�$��december���� -   $
 CurrentYearLabel��$��0��$��1��$��Center=�$
��Bold"�$��14(��� -   $
 TotalJanLabel��$��1��$��1��$��Center=�$
��Bold"�$��14(����� *TotalCustomersJan+$	 Window���� -   $
 TotalFebLabel��$��2��$��1��$��Center=�$
��Bold"�$��14(����� *TotalCustomersFeb+$	 Window���� -   $
 TotalMarLabel��$��3��$��1��$��Center=�$
��Bold"�$��14(����� *TotalCustomersMar+$	 Window���� -   $
 TotalAprLabel��$��4��$��1��$��Center=�$
��Bold"�$��14(����� *TotalCustomersApr+$	 Window���� -   $
 TotalMayLabel��$��5��$��1��$��Center=�$
��Bold"�$��14(����� *TotalCustomersMay+$	 Window���� -   $
 TotalJunLabel��$��6��$��1��$��Center=�$
��Bold"�$��14(����� *TotalCustomersJun+$	 Window���� -   $
 TotalJulLabel��$��7��$��1��$��Center=�$
��Bold"�$��14(����� *TotalCustomersJul+$	 Window���� -   $
 TotalAugLabel��$��8��$��1��$��Center=�$
��Bold"�$��14(����� *TotalCustomersAug+$	 Window���� -    $
 TotalSepLabel��$��9��$��1��$��Center=�$
��Bold"�$��14(����� *TotalCustomersSep+$	 Window���� -!   $
 TotalOctLabel��$��10��$��1��$��Center=�$
��Bold"�$��14(����� *TotalCustomersOct+$	 Window���� -"   $
 TotalNovLabel��$��11��$��1��$��Center=�$
��Bold"�$��14(����� *TotalCustomersNov+$	 Window���� -#   $
 TotalDecLabel��$��12��$��1��$��Center=�$
��Bold"�$��14(����� *TotalCustomersDec+$	 Window���� -$   $
 PreviousYearLabel��$��0��$��2��$��Center=�$
��Bold"�$��14(��� -%   $
 TotalPreviousJanLabel��$��1��$��2��$��Center=�$
��Bold"�$��14(����� *TotalCustomersPreviousJan+$	 Window���� -&   $
 TotalPreviousFebLabel��$��2��$��2��$��Center=�$
��Bold"�$��14(����� *TotalCustomersPreviousFeb+$	 Window���� -'   $
 TotalPreviousMarLabel��$��3��$��2��$��Center=�$
��Bold"�$��14(����� *TotalCustomersPreviousMar+$	 Window���� -(   $
 TotalPreviousAprLabel��$��4��$��2��$��Center=�$
��Bold"�$��14(����� *TotalCustomersPreviousApr+$	 Window���� -)   $
 TotalPreviousMayLabel��$��5��$��2��$��Center=�$
��Bold"�$��14(����� *TotalCustomersPreviousMay+$	 Window���� -*   $
 TotalPreviousJunLabel��$��6��$��2��$��Center=�$
��Bold"�$��14(����� *TotalCustomersPreviousJun+$	 Window���� -+   $
 TotalPreviousJulLabel��$��7��$��2��$��Center=�$
��Bold"�$��14(����� *TotalCustomersPreviousJul+$	 Window���� -,   $
 TotalPreviousAugLabel��$��8��$��2��$��Center=�$
��Bold"�$��14(����� *TotalCustomersPreviousAug+$	 Window���� --   $
 TotalPreviousSepLabel��$��9��$��2��$��Center=�$
��Bold"�$��14(����� *TotalCustomersPreviousSep+$	 Window���� -.   $
 TotalPreviousOctLabel��$��10��$��2��$��Center=�$
��Bold"�$��14(����� *TotalCustomersPreviousOct+$	 Window���� -/   $
 TotalPreviousNovLabel��$��11��$��2��$��Center=�$
��Bold"�$��14(����� *TotalCustomersPreviousNov+$	 Window���� -0   $
 TotalPreviousDecLabel��$��12��$��2��$��Center=�$
��Bold"�$��14(����� *TotalCustomersPreviousDec+$	 Window���� $��Verschil��$��0��$��3��$��Center=�$
��Bold"�$��14(��� $��1��$��3��$��Center=�$
��Bold"�$��14(����� *DifferenceJan+$	 Window������ *DifferenceJan+$	 Window�� �� 	Converter# [  �� $��2��$��3��$��Center=�$
��Bold"�$��14(����� *DifferenceFeb+$	 Window������ *DifferenceFeb+$	 Window��# [  �� $��3��$��3��$��Center=�$
��Bold"�$��14(����� *DifferenceMar+$	 Window������ *DifferenceMar+$	 Window��# [  �� $��4��$��3��$��Center=�$
��Bold"�$��14(����� *DifferenceApr+$	 Window������ *DifferenceApr+$	 Window��# [  �� $��5��$��3��$��Center=�$
��Bold"�$��14(����� *DifferenceMay+$	 Window������ *DifferenceMay+$	 Window��# [  �� $��6��$��3��$��Center=�$
��Bold"�$��14(����� *DifferenceJun+$	 Window������ *DifferenceJun+$	 Window��# [  �� $��7��$��3��$��Center=�$
��Bold"�$��14(����� *DifferenceJul+$	 Window������ *DifferenceJul+$	 Window��# [  �� $��8��$��3��$��Center=�$
��Bold"�$��14(����� *DifferenceAug+$	 Window������ *DifferenceAug+$	 Window��# [  �� $��9��$��3��$��Center=�$
��Bold"�$��14(����� *DifferenceSep+$	 Window������ *DifferenceSep+$	 Window��# [  �� $��10��$��3��$��Center=�$
��Bold"�$��14(����� *DifferenceOct+$	 Window������ *DifferenceOct+$	 Window��# [  �� $��11��$��3��$��Center=�$
��Bold"�$��14(����� *DifferenceNov+$	 Window������ *DifferenceNov+$	 Window��# [  �� $��12��$��3��$��Center=�$
��Bold"�$��14(����� *DifferenceDec+$	 Window������ *DifferenceDec+$	 Window��# [  �� ���� $��Jaaroverzicht��.��� $��10q�.+�#  LiveCharts.Wpf.CartesianChart  -1   $��	LineChart��&   LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Right=�   Series �� *SeriesCollection+   AxisY   LiveCharts.Wpf.Axis     Title$ Klanten��   MinValue$ 0Q�   Labels �� *Values+   AxisX   $ Maand�� �� *Labels+