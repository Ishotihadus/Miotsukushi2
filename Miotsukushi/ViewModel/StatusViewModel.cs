using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miotsukushi.Model.KanColle;

namespace Miotsukushi.ViewModel
{
    class StatusViewModel : ViewModelBase
    {
        Random random = new Random();

        private string _StatusText = "";
        public string StatusText
        {
            get
            {
                return _StatusText;
            }

            set
            {
                if (_StatusText != value)
                {
                    _StatusText = value;
                    OnPropertyChanged(() => StatusText);
                }
            }
        }

        private string _StatusAlertTitle;
        public string StatusAlertTitle
        {
            get
            {
                return _StatusAlertTitle;
            }

            set
            {
                if (_StatusAlertTitle != value)
                {
                    _StatusAlertTitle = value;
                    
                    switch(value)
                    {
                        case "Notify":
                            StatusAlertBackground = System.Windows.Media.Brushes.DodgerBlue;
                            break;
                        case "Info":
                            StatusAlertBackground = System.Windows.Media.Brushes.SeaGreen;
                            break;
                        case "Error":
                            StatusAlertBackground = System.Windows.Media.Brushes.Crimson;
                            break;
                        case "Success":
                        case "Win":
                            StatusAlertBackground = System.Windows.Media.Brushes.RoyalBlue;
                            break;
                        case "Failed":
                        case "Lose":
                            StatusAlertBackground = System.Windows.Media.Brushes.LightCoral;
                            break;
                        default:
                            StatusAlertBackground = System.Windows.Media.Brushes.DarkMagenta;
                            break;
                    }
                    OnPropertyChanged(() => StatusAlertTitle);
                }
            }
        }

        private string _StatusAlertText;
        public string StatusAlertText
        {
            get
            {
                return _StatusAlertText;
            }

            set
            {
                StatusCode = random.Next().ToString();
                if (_StatusAlertText != value)
                {
                    _StatusAlertText = value;
                    OnPropertyChanged(() => StatusAlertText);
                }
            }
        }

        private System.Windows.Media.Brush _StatusAlertBackground;
        public System.Windows.Media.Brush StatusAlertBackground
        {
            get
            {
                return _StatusAlertBackground;
            }

            set
            {
                if (_StatusAlertBackground != value)
                {
                    _StatusAlertBackground = value;
                    OnPropertyChanged(() => StatusAlertBackground);
                }
            }
        }

        private string _StatusCode;
        public string StatusCode
        {
            get
            {
                return _StatusCode;
            }

            set
            {
                if (_StatusCode != value)
                {
                    _StatusCode = value;
                    OnPropertyChanged(() => StatusCode);
                }
            }
        }


        KanColleModel model;

        public StatusViewModel()
        {
            model = Model.MainModel.Current.KancolleModel;

            StatusText = "艦これの起動を待機しています...";

            ((App)App.Current).UnhandledExceptionRaised += (_, e) =>
                {
                    StatusAlertTitle = "Error";
                    StatusAlertText = "ハンドルされていない例外が発生しました。";
                };

            model.InitializeComplete += (_, __) =>
                {
                    StatusText = "準備完了";
                    StatusAlertTitle = "Notify";
                    StatusAlertText = "艦これが起動しました。サーバーは " + (model.Serverinfo.GetServerName() ?? "（不明）") + " です。";
                };

            model.ApiAnalyzeError += (_, e) =>
                {
                    StatusAlertTitle = "Error";
                    StatusAlertText = e.Kcsapiurl + " の解析に失敗しました。";
                };

            model.UnknownApiReceived += (_, e) =>
            {
                StatusAlertTitle = "Info";
                StatusAlertText = "未知のAPI " + e.Kcsapiurl + " を受信しました。";
            };

            model.GetFiddlerLog += (_, e) =>
                {
                    StatusAlertTitle = "Fiddler";
                    StatusAlertText = e.Message;
                };

            model.CreateItem += (_, e) =>
                {
                    StatusAlertTitle = e.Success ? "Success" : "Failed";
                    StatusAlertText = (e.Name ?? "（不明）") + " の開発に" + (e.Success ? "成功" : "失敗") + "しました。"; 
                };

            model.Battlemodel.GetBattleResult += (_, e) =>
                {
                    var win = false;
                    var rankname = "";
                    switch (e.Rank)
                    {
                        case "S":
                        case "A":
                        case "B":
                            win = true;
                            break;
                    }
                    switch (e.Rank)
                    {
                        case "B":
                        case "C":
                            rankname = "戦術的";
                            break;
                    }

                    rankname += (win ? "勝利" : "敗北") + e.Rank;
                    StatusAlertTitle = win ? "Win" : "Lose";
                    StatusAlertText = rankname + "。ドロップ艦は" + (e.HasGetShip ? " " + e.GetShipName + " です。" : "ありません。");
                };
        }
    }
}
