﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miotsukushi.Model.KanColle;

namespace Miotsukushi.ViewModel
{
    class StatusViewModel : ViewModelBase
    {
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
                        case "Error":
                            StatusAlertBackground = System.Windows.Media.Brushes.Crimson;
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

        KanColleModel model;

        public StatusViewModel()
        {
            model = Model.MainModel.Current.kancolleModel;

            StatusText = "艦これの起動を待機しています...";

            model.InitializeComplete += (_, __) =>
                {
                    StatusText = "準備完了";
                    StatusAlertTitle = "Notify";
                    StatusAlertText = "艦これが起動しました。";
                };

            model.APIAnalyzeError += (_, e) =>
                {
                    StatusAlertTitle = "Error";
                    StatusAlertText = e.kcsapiurl + " の解析に失敗しました。";
                };

            model.GetFiddlerLog += (_, e) =>
                {
                    StatusAlertTitle = "Fiddler";
                    StatusAlertText = e.message;
                };
        }
    }
}
