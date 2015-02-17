using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        public StatusViewModel Status { get; set; }
        public WindowCommandsViewModel Commands { get; set; }

        public MainViewModel()
        {
            Status = new StatusViewModel();
            Commands = new WindowCommandsViewModel();
        }
    }
}
