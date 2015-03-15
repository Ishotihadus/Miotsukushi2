using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Miotsukushi.ViewModel.EasyInfoPanel
{
    class QuestViewModel : ViewModelBase
    {
        #region プロパティの定義

        private bool _DetailVisibility;
        public bool DetailVisibility
        {
            get
            {
                return _DetailVisibility;
            }

            set
            {
                if (_DetailVisibility != value)
                {
                    _DetailVisibility = value;
                    OnPropertyChanged(() => DetailVisibility);
                }
            }
        }

        private Brush _BorderBrush;
        public Brush BorderBrush
        {
            get
            {
                return _BorderBrush;
            }

            set
            {
                if (_BorderBrush != value)
                {
                    _BorderBrush = value;
                    OnPropertyChanged(() => BorderBrush);
                }
            }
        }

        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }

        private string _Description;
        public string Description
        {
            get
            {
                return _Description;
            }

            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged(() => Description);
                }
            }
        }


        private string _Category;
        public string Category
        {
            get
            {
                return _Category;
            }

            set
            {
                if (_Category != value)
                {
                    _Category = value;
                    OnPropertyChanged(() => Category);
                }
            }
        }

        private string _Type;
        public string Type
        {
            get
            {
                return _Type;
            }

            set
            {
                if (_Type != value)
                {
                    _Type = value;
                    OnPropertyChanged(() => Type);
                }
            }
        }

        private string _ProgressText;
        public string ProgressText
        {
            get
            {
                return _ProgressText;
            }

            set
            {
                if (_ProgressText != value)
                {
                    _ProgressText = value;
                    OnPropertyChanged(() => ProgressText);
                }
            }
        }

        private double _ProgressValue;
        public double ProgressValue
        {
            get
            {
                return _ProgressValue;
            }

            set
            {
                if (_ProgressValue != value)
                {
                    _ProgressValue = value;
                    OnPropertyChanged(() => ProgressValue);
                }
            }
        }


        private string _Message;
        public string Message
        {
            get
            {
                return _Message;
            }

            set
            {
                if (_Message != value)
                {
                    _Message = value;
                    OnPropertyChanged(() => Message);
                }
            }
        }

        #endregion

        readonly string[] category_str = new[] { "", "編成", "出撃", "演習", "遠征", "補給/入渠", "工廠", "改装" };
        readonly SolidColorBrush[] category_brush = new[]
        {
            Brushes.Gray, Brushes.ForestGreen, Brushes.Crimson, Brushes.MediumSeaGreen, Brushes.DeepSkyBlue,
            Brushes.Goldenrod, Brushes.Sienna, Brushes.MediumOrchid
        };

        readonly string[] type_str = new[] { "", "", "デイリー", "ウィークリー", "デイリー", "デイリー", "マンスリー" };

        public QuestViewModel(Miotsukushi.Model.KanColle.QuestData.Quest quest)
        {
            if(quest == null)
            {
                DetailVisibility = false;
                Message = "未取得の任務";
                BorderBrush = category_brush[0];
            }
            else
            {
                DetailVisibility = true;
                Name = quest.name;
                Description = quest.description;

                if(quest.category >= 1 && quest.category < category_str.Length)
                {
                    Category = category_str[quest.category];
                    BorderBrush = category_brush[quest.category];
                }
                else
                {
                    Category = category_str[0];
                    BorderBrush = category_brush[0];
                }

                if (quest.type >= 1 && quest.type < type_str.Length)
                    Type = type_str[quest.type];
                else
                    Type = type_str[0];

                if(quest.state == 3)
                {
                    ProgressText = "達成";
                    ProgressValue = 1.0;
                }
                else
                {
                    switch(quest.progress)
                    {
                        case 1:
                            ProgressText = "50%";
                            ProgressValue = 0.5;
                            break;
                        case 2:
                            ProgressText = "80%";
                            ProgressValue = 0.8;
                            break;
                        default:
                            ProgressText = "";
                            ProgressValue = 0;
                            break;
                    }
                }
            }

        }

    }
}
