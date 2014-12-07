using ArtNet.Common;
using ArtNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ArtNet.Fascade
{
    public class ArtNetFascade
    {
        public short Universe { get { return package.Universe; } }
        private ArtDmxPackage package;
        private Persistence persistence;
        private Sender sender;
        private Timer sendTimer;
        private Timer saveTimer;
        private bool hasChangedSinceLastSend;
        private bool hasChangedSinceLastSave;
        private object saveLock = new object();

        //private IDictionary<short, Fade> fades;

        public ArtNetFascade(short universe, string host)
        {
            persistence = new Persistence();
            package = persistence.Get(universe);
            sender = new Sender(host);
            sendTimer = new Timer(10);
            sendTimer.Elapsed += sendTimer_Elapsed;
            sendTimer.Start();
            saveTimer = new Timer(2000);
            saveTimer.Elapsed += saveTimer_Elapsed;
            saveTimer.Start();
            //fades = new Dictionary<short, Fade>();
        }

        void saveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (hasChangedSinceLastSave)
            {
                hasChangedSinceLastSave = false;
                lock (saveLock)
                {
                    persistence.UpdateAsync(package).Wait();
                }
            }
        }

        async void sendTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if(hasChangedSinceLastSend)
            {
                hasChangedSinceLastSend = false;
                await this.sender.SendAsync(package);
            }
        }

        //public void Fade(short address, byte value, double milliSeconds)
        //{
        //    var originalValue = package.data[address - 1];
        //    var fade = new Fade(address, value, originalValue, milliSeconds);
        //    fades[address] = fade;
        //}

        public void Set(short address, byte value)
        {
            if (Get(address) != value)
            {
                package.Set(address, value);
                hasChangedSinceLastSend = true;
                hasChangedSinceLastSave = true;
            }
        }

        public void Set(params AddressValuePair[] pairs)
        {
            var anyChange = pairs.Any(p => Get(p.Address) != p.Value);
            if (anyChange)
            {
                package.Set(pairs);
                hasChangedSinceLastSend = true;
                hasChangedSinceLastSave = true;
            }
        }

        public byte Get(short address)
        {
            if (address < 1 || address > 512)
                throw new ArgumentOutOfRangeException("address");

            return package.data[address - 1];
        }
    }
}
