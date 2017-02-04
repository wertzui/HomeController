using ArtNet.Common;
using ArtNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace ArtNet.Fascade
{
    /// <summary>
    /// Fascade to communicate with the ArtNet.
    /// </summary>
    public class ArtNetFascade
    {
        bool hasChangedSinceLastSave;
        bool hasChangedSinceLastSend;
        bool hasRunningFade;
        ArtDmxPackage package; // this is the current value
        ArtDmxPackage targetPackage; // we want to fade to this value
        Persistence persistence;
        object saveLock = new object();
        Timer saveTimer;
        Sender sender;
        Timer sendTimer;
        Timer fadeTimer;

        //private IDictionary<short, Fade> fades;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtNetFascade"/> class.
        /// </summary>
        /// <param name="universe">The universe.</param>
        /// <param name="hosts">The hosts.</param>
        public ArtNetFascade(short universe, ISet<string> hosts)
        {
            persistence = new Persistence();
            package = persistence.GetAsync(universe).Result;
            targetPackage = persistence.GetAsync(universe).Result;
            sender = new Sender(hosts);
            sendTimer = new Timer(10);
            sendTimer.Elapsed += sendTimer_Elapsed;
            sendTimer.Start();
            saveTimer = new Timer(2000);
            saveTimer.Elapsed += saveTimer_Elapsed;
            saveTimer.Start();
            fadeTimer = new Timer(100);
            fadeTimer.Elapsed += FadeTimer_Elapsed;
            fadeTimer.Start();
            //fades = new Dictionary<short, Fade>();
        }

        private void FadeTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (hasRunningFade)
            {
                hasRunningFade = false;
                byte current;
                byte target;
                for (short i = ArtDmxPackage.MinAddress; i <= ArtDmxPackage.MaxAddress; i++)
                {
                    current = package.Get(i);
                    target = targetPackage.Get(i);
                    if (current < target)
                    {
                        package.Set(i, (byte)(current + 1));
                        hasChangedSinceLastSend = true;
                        hasRunningFade = true;
                    }
                    else if (current > target)
                    {
                        package.Set(i, (byte)(current - 1));
                        hasChangedSinceLastSend = true;
                        hasRunningFade = true;
                    }
                }
            }
        }

        public short Universe { get { return package.Universe; } }

        /// <summary>
        /// Gets the value for the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public byte Get(short address)
        {
            if (address < 1 || address > 512)
                throw new ArgumentOutOfRangeException(nameof(address));

            return package.Data[address - 1];
        }

        /// <summary>
        /// Sets the specified address to the specified value.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="value">The value.</param>
        public void Set(short address, byte value)
        {
            if (Get(address) != value)
            {
                targetPackage.Set(address, value);
                hasChangedSinceLastSend = true;
                hasChangedSinceLastSave = true;
                hasRunningFade = true;
            }
        }

        /// <summary>
        /// Sets the specified addresses to the specified values.
        /// </summary>
        /// <param name="pairs">The address value pairs.</param>
        public void Set(params AddressValuePair[] pairs)
        {
            Set((IEnumerable<AddressValuePair>)pairs);
        }

        /// <summary>
        /// Sets the specified addresses to the specified values.
        /// </summary>
        /// <param name="pairs">The address value pairs.</param>
        public void Set(IEnumerable<AddressValuePair> pairs)
        {
            if (pairs != null)
            {
                var anyChange = pairs.Any(p => Get(p.Address) != p.Value);
                if (anyChange)
                {
                    targetPackage.Set(pairs);
                    hasChangedSinceLastSend = true;
                    hasChangedSinceLastSave = true;
                    hasRunningFade = true;
                }
            }
        }

        /// <summary>
        /// Handles the Elapsed event of the saveTimer control and saves changes to the data base.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
        void saveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (hasChangedSinceLastSave)
            {
                hasChangedSinceLastSave = false;
                lock (saveLock)
                {
                    persistence.UpdateAsync(targetPackage).Wait();
                }
            }
        }

        /// <summary>
        /// Handles the Elapsed event of the sendTimer control and sends any new values to the ArtNet.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
        async void sendTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (hasChangedSinceLastSend)
            {
                hasChangedSinceLastSend = false;
                await this.sender.SendAsync(package);
            }
        }
    }
}