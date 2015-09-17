﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;

namespace Miotsukushi.ViewModel.CheatWindow
{
    class DebuggerViewModel : ViewModelBase
    {
        private string _Kcsapiurl;
        public string Kcsapiurl
        {
            get
            {
                return _Kcsapiurl;
            }

            set
            {
                if (_Kcsapiurl != value)
                {
                    _Kcsapiurl = value;
                    OnPropertyChanged(() => Kcsapiurl);
                    RaiseEventCommand.RaiseCanExecuteChanged();
                }
            }
        }


        private string _Request;
        public string Request
        {
            get
            {
                return _Request;
            }

            set
            {
                if (_Request != value)
                {
                    _Request = value;
                    OnPropertyChanged(() => Request);
                    RaiseEventCommand.RaiseCanExecuteChanged();
                }
            }
        }


        private string _Response;
        public string Response
        {
            get
            {
                return _Response;
            }

            set
            {
                if (_Response != value)
                {
                    _Response = value;
                    OnPropertyChanged(() => Response);
                    RaiseEventCommand.RaiseCanExecuteChanged();
                }
            }
        }


        private DelegateCommand _RaiseEventCommand;
        public DelegateCommand RaiseEventCommand
        {
            get
            {
                return _RaiseEventCommand ?? (_RaiseEventCommand = new DelegateCommand(() =>
                {
                    Model.MainModel.Current.KancolleModel.Debuggermodel.RaiseEvent(Kcsapiurl, Request, Response);
                    Kcsapiurl = "";
                    Request = "";
                    Response = "";
                },
                    () => Model.MainModel.Current.KancolleModel.Debuggermodel.IsAvailable(Kcsapiurl, Request, Response)));
            }
        }

        private DelegateCommand _RaiseEventFromFileCommand;
        public DelegateCommand RaiseEventFromFileCommand
        {
            get
            {
                return _RaiseEventFromFileCommand ??
                       (_RaiseEventFromFileCommand =
                           new DelegateCommand(
                               () => Model.MainModel.Current.KancolleModel.Debuggermodel.RaiseEventFromFileAsync(),
                               () => true));
            }
        }

        public DebuggerViewModel()
        {
            Kcsapiurl = "";
            Request = "";
            Response = "";
        }
    }
}
