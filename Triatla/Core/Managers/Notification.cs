using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triatla.Core.Managers
{
    public class Notification
    {
        public static async void ShowNotification(object param, int delay)
        {
            if (!(param.ToString() is string p)) return;



            await Task.Delay(delay);
        }
    }
}
