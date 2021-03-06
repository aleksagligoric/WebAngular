﻿using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using WebApp.Models;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Hubs
{
    [HubName("notifications")]
    public class LokacijaVozilaHub : Hub
    {
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<LokacijaVozilaHub>();

        private static List<Stanica> stanice = new List<Stanica>();
        private static int stanicaBrojac = 0;
        private static Timer timer = new Timer();

        public LokacijaVozilaHub()
        {

        }

        public void GetTime()
        {
            //Svim klijentima se salje setRealTime poruka
            Clients.All.setRealTime(DateTime.Now.ToString("h:mm:ss tt"));
        }

        public void StartLocationServerUpdates()
        {
            if (timer.Interval != 4000)
            {
                timer.Interval = 4000;
                //timer.Start();
                timer.Elapsed += OnTimedEvent;
            }
            timer.Enabled = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            StringBuilder busData = new StringBuilder("");


            if (stanice != null)
            {

                if (stanicaBrojac >= stanice.Count)
                {
                    stanicaBrojac = 0;
                }
                double[] niz = { stanice[stanicaBrojac].X, stanice[stanicaBrojac].Y };
                busData.Append($"{stanice[stanicaBrojac].X}_{stanice[stanicaBrojac].Y};");
                Clients.All.getBusData(niz);
                stanicaBrojac++;

                (source as Timer).Enabled = true;
            }

        }

        private void Lokacija()
        {
            StringBuilder busData = new StringBuilder("");

            if (stanice != null)
            {
                if (stanicaBrojac >= stanice.Count)
                {
                    stanicaBrojac = 0;
                }
                double[] niz = { stanice[stanicaBrojac].X, stanice[stanicaBrojac].Y };
                //Clients.All.setRealTime(niz);
                busData.Append($"{stanice[stanicaBrojac].X}_{stanice[stanicaBrojac].Y};");
                Clients.All.getBusData(busData.ToString());
                stanicaBrojac++;
            }

        }

        public void StopLocationServerUpdates()
        {
            timer.Stop();
            stanice = null;
            //stanicaBrojac = 0;
        }

        public void DodajStanice(List<Stanica> staniceIzKontrolera)
        {
            stanice = new List<Stanica>();
            stanice = staniceIzKontrolera;
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }
    }
}