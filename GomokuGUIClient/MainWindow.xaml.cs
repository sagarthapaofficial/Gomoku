using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading;
using System.ServiceModel;
using GomokuLibrary;

namespace GomokuGUIClient
{
    /**
      * Class Name: MainWindow.xaml.cs		
      * Purpose: A multiplayer game application which incorporates multiple assemblies and WCF
      * Coders: Haris Khalid, Woojin Shin, Sterling Gault, Sagar Thapa
      * Date: Apr 3rd, 2022
      */
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, UseSynchronizationContext = false)]
    public partial class MainWindow : Window, ICallback
    {
        private IGame game = null;
        //C'tor
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                // Connect to the Game service and subscribe to the callbacks 
                DuplexChannelFactory<IGame> channel = new DuplexChannelFactory<IGame>(this, "GameEndPoint");
                game = channel.CreateChannel();
                game.RegisterForCallbacks();
                //reset contents on the board
                ResetContents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //create a new game repopulating and resetting all values
            game.Repopulate();
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ResetContents()
        {
            //clear the game board by looping through each cells and clearing them
            GameBoardContainer.Children.Cast<Button>().ToList().ForEach(grid =>
            {
                grid.Content = "";
                grid.Background = Brushes.Transparent;
                grid.IsEnabled = true;
                grid.Opacity = 1;
            });
            //reset the score textboxes 
            TextBoxPlayerOneScore.Background = Brushes.LightGoldenrodYellow;
            TextBoxPlayerTwoScore.Background = Brushes.White;
            //reset the results
            TextBoxResult.Text = "";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if game is over then return nothing
                if (game.GameEnd)
                {
                    return; 
                }
                var button = (Button)sender;
                //find the position of where the player places symbol X or O mark
                var row = Grid.GetRow(button);
                var column = Grid.GetColumn(button);
                //get the index value
                var index = column + (row * 5); //board is 5x5
                //get player's turn
                var playerOneTurn = game.PlayerOneTurn;
                //play and get which player's mark value was landed on the board
                Mark mark = game.Play(playerOneTurn, index);
                if (mark != null)
                {
                    //assign the content position on board
                    button.Content = mark;
                    //check for a winner
                    game.CheckWinner();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            game?.UnregisterFromCallbacks();
        }
        public void UpdateGui(CallbackInfo info)
        {
            if (Thread.CurrentThread == this.Dispatcher.Thread)
            {
                Mark mark = info.SelectedMark;
                if (mark != null)
                {
                    switch (mark.CellPosition)
                    {
                        case 0:
                            Button0x0.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button0x0.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button0x0.Foreground = Brushes.Black;
                            }
                            break;
                        case 1:
                            Button0x1.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button0x1.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button0x1.Foreground = Brushes.Black;
                            }
                            break;
                        case 2:
                            Button0x2.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button0x2.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button0x2.Foreground = Brushes.Black;
                            }
                            break;
                        case 3:
                            Button0x3.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button0x3.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button0x3.Foreground = Brushes.Black;
                            }
                            break;
                        case 4:
                            Button0x4.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button0x4.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button0x4.Foreground = Brushes.Black;
                            }
                            break;
                        case 5:
                            Button1x0.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button1x0.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button1x0.Foreground = Brushes.Black;
                            }
                            break;
                        case 6:
                            Button1x1.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button1x1.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button1x1.Foreground = Brushes.Black;
                            }
                            break;
                        case 7:
                            Button1x2.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button1x2.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button1x2.Foreground = Brushes.Black;
                            }
                            break;
                        case 8:
                            Button1x3.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button1x3.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button1x3.Foreground = Brushes.Black;
                            }
                            break;
                        case 9:
                            Button1x4.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button1x4.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button1x4.Foreground = Brushes.Black;
                            }
                            break;
                        case 10:
                            Button2x0.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button2x0.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button2x0.Foreground = Brushes.Black;
                            }
                            break;
                        case 11:
                            Button2x1.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button2x1.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button2x1.Foreground = Brushes.Black;
                            }
                            break;
                        case 12:
                            Button2x2.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button2x2.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button2x2.Foreground = Brushes.Black;
                            }
                            break;
                        case 13:
                            Button2x3.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button2x3.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button2x3.Foreground = Brushes.Black;
                            }
                            break;
                        case 14:
                            Button2x4.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button2x4.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button2x4.Foreground = Brushes.Black;
                            }
                            break;
                        case 15:
                            Button3x0.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button3x0.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button3x0.Foreground = Brushes.Black;
                            }
                            break;
                        case 16:
                            Button3x1.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button3x1.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button3x1.Foreground = Brushes.Black;
                            }
                            break;
                        case 17:
                            Button3x2.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button3x2.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button3x2.Foreground = Brushes.Black;
                            }
                            break;
                        case 18:
                            Button3x3.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button3x3.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button3x3.Foreground = Brushes.Black;
                            }
                            break;
                        case 19:
                            Button3x4.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button3x4.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button3x4.Foreground = Brushes.Black;
                            }
                            break;
                        case 20:
                            Button4x0.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button4x0.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button4x0.Foreground = Brushes.Black;
                            }
                            break;
                        case 21:
                            Button4x1.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button4x1.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button4x1.Foreground = Brushes.Black;
                            }
                            break;
                        case 22:
                            Button4x2.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button4x2.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button4x2.Foreground = Brushes.Black;
                            }
                            break;
                        case 23:
                            Button4x3.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button4x3.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button4x3.Foreground = Brushes.Black;
                            }
                            break;
                        case 24:
                            Button4x4.Content = mark;
                            if (info.PlayerOneTurn)
                            {
                                Button4x4.Foreground = Brushes.Red;
                            }
                            else
                            {
                                Button4x4.Foreground = Brushes.Black;
                            }
                            break;
                        default:
                            break;
                    }
                    if (!info.GameEnd)
                    {
                        //update whos turn is it
                        if (info.PlayerOneTurn)
                        {
                            //change background color to indicate the next player turn
                            TextBoxResult.Text = "Player One's Turn!";
                            TextBoxPlayerOneScore.Background = Brushes.LightGoldenrodYellow;
                            TextBoxPlayerTwoScore.Background = Brushes.White;
                        }
                        else
                        {
                            TextBoxResult.Text = "Player Two's Turn!";
                            TextBoxPlayerOneScore.Background = Brushes.White;
                            TextBoxPlayerTwoScore.Background = Brushes.LightGoldenrodYellow;
                        }
                    }
                    else
                    {
                        //update result
                        TextBoxResult.Text = info.Results.ToString();
                        //update the score textboxes
                        TextBoxPlayerOneScore.Text = info.PlayerOneScore.ToString();
                        TextBoxPlayerTwoScore.Text = info.PlayerTwoScore.ToString();
                        //winning cells
                        List<int> marks = info.WinningCells;
                        if (marks.Count != 0) //if the game is over
                        {
                            //clear the board along with the buttons background
                            GameBoardContainer.Children.Cast<Button>().ToList().ForEach(grid =>
                            {
                                grid.IsEnabled = false;
                                grid.Opacity = 0.2;

                            });
                            foreach (var i in marks)
                            {
                                switch (i)
                                {
                                    case 0:
                                        Button0x0.Background = Brushes.Goldenrod;
                                        Button0x0.Opacity = 0.5;
                                        break;
                                    case 1:
                                        Button0x1.Background = Brushes.Goldenrod;
                                        Button0x1.Opacity = 0.5;
                                        break;
                                    case 2:
                                        Button0x2.Background = Brushes.Goldenrod;
                                        Button0x2.Opacity = 0.5;
                                        break;
                                    case 3:
                                        Button0x3.Background = Brushes.Goldenrod;
                                        Button0x3.Opacity = 0.5;
                                        break;
                                    case 4:
                                        Button0x4.Background = Brushes.Goldenrod;
                                        Button0x4.Opacity = 0.5;
                                        break;
                                    case 5:
                                        Button1x0.Background = Brushes.Goldenrod;
                                        Button1x0.Opacity = 0.5;
                                        break;
                                    case 6:
                                        Button1x1.Background = Brushes.Goldenrod;
                                        Button1x1.Opacity = 0.5;
                                        break;
                                    case 7:
                                        Button1x2.Background = Brushes.Goldenrod;
                                        Button1x2.Opacity = 0.5;
                                        break;
                                    case 8:
                                        Button1x3.Background = Brushes.Goldenrod;
                                        Button1x3.Opacity = 0.5;
                                        break;
                                    case 9:
                                        Button1x4.Background = Brushes.Goldenrod;
                                        Button1x4.Opacity = 0.5;
                                        break;
                                    case 10:
                                        Button2x0.Background = Brushes.Goldenrod;
                                        Button2x0.Opacity = 0.5;
                                        break;
                                    case 11:
                                        Button2x1.Background = Brushes.Goldenrod;
                                        Button2x1.Opacity = 0.5;
                                        break;
                                    case 12:
                                        Button2x2.Background = Brushes.Goldenrod;
                                        Button2x2.Opacity = 0.5;
                                        break;
                                    case 13:
                                        Button2x3.Background = Brushes.Goldenrod;
                                        Button2x3.Opacity = 0.5;
                                        break;
                                    case 14:
                                        Button2x4.Background = Brushes.Goldenrod;
                                        Button2x4.Opacity = 0.5;
                                        break;
                                    case 15:
                                        Button3x0.Background = Brushes.Goldenrod;
                                        Button3x0.Opacity = 0.5;
                                        break;
                                    case 16:
                                        Button3x1.Background = Brushes.Goldenrod;
                                        Button3x1.Opacity = 0.5;
                                        break;
                                    case 17:
                                        Button3x2.Background = Brushes.Goldenrod;
                                        Button3x2.Opacity = 0.5;
                                        break;
                                    case 18:
                                        Button3x3.Background = Brushes.Goldenrod;
                                        Button3x3.Opacity = 0.5;
                                        break;
                                    case 19:
                                        Button3x4.Background = Brushes.Goldenrod;
                                        Button3x4.Opacity = 0.5;
                                        break;
                                    case 20:
                                        Button4x0.Background = Brushes.Goldenrod;
                                        Button4x0.Opacity = 0.5;
                                        break;
                                    case 21:
                                        Button4x1.Background = Brushes.Goldenrod;
                                        Button4x1.Opacity = 0.5;
                                        break;
                                    case 22:
                                        Button4x2.Background = Brushes.Goldenrod;
                                        Button4x2.Opacity = 0.5;
                                        break;
                                    case 23:
                                        Button4x3.Background = Brushes.Goldenrod;
                                        Button4x3.Opacity = 0.5;
                                        break;
                                    case 24:
                                        Button4x4.Background = Brushes.Goldenrod;
                                        Button4x4.Opacity = 0.5;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (!info.GameEnd)
                    {
                        //reset the contents on the board
                        ResetContents();
                    }
                }
            }
            else
            {
                Action<CallbackInfo> updateDelegate = UpdateGui;
                this.Dispatcher.BeginInvoke(updateDelegate, info);
            }
        }
    }
}
