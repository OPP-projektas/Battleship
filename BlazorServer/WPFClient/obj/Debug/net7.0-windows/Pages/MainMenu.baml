   M S B A M L   `   `   `  ����D  @WPFClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WPFClient.PagesWPFClient.Pages  5      ;"clr-namespace:WPFClient.ComponentsWPFClient.Components  5          WPFClient.Pages.MainMenu   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:WPFClient.Pages     5      9
components"clr-namespace:WPFClient.Components     5        N� Title$  MainMenu��5
      .��5      � -   5         �Name$ MenuGrid��5      
���iii� � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $	��.3* �6   .+�5   
   �� -   5         ��Name$ openConnection��5      $��0��6)   $��Center=�66   $��20 10q�5      $��20q�6!   .��6J   Open Connection5      �� 5   
    �� Orientation$ 
Horizontal=�6   $��1��6.   $��Center=�6;   .+�5      �� -   5         ��Name$ messageInput��5      $	��300��6,   $��20q�6;   �� 5      -   5        $ sendMessage��5      $��0��6*   $��Center=�67   $��20 10q�5      $��20q�6!   .��6G   Send Message5!      5"      �� 5$   
   $��2��6   $��20q�6$   $	��200��60   $
��Auto=�6@   .��5%      �� -   5         ��Name$ messages��5%      $��Stretch=�6(   $��20 10q�6F   $��0q�6V   5&      �� 5(   
   $��3��6   $��Center=�6"   $ 
Horizontal=�6?   .+�5)      �� -   5        $ openCounter��5)      $��20 10q�6*   $��20q�6:   .��6`   Open Counter Connection6y   �� 5*      -   5        $ incrementCounter��5*      $��20 10q�6/   $��20q�6?   .��6j   Increment Counter6}   5+      �� 5-   
   $��4��6   $��Center=�6"   $ 
Horizontal=�6?   .+�5.      �� -   5        $ 	startGame��5.      $��20 10q�6(   $��20q�68   .��6]   
Start Game6i   5/         WPFClient.Components.Log  51   
   -	   5          Name$ loggerMessages��51   &   $��5��6   6&   53      54      