using Ajanda_Projesi.Models;
using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Ajanda_Projesi.Controllers
{
    public class OlayController : Controller
    {
        public ActionResult Index()
        {
            var ajanda = new DHXScheduler(this);
            ajanda.Skin = DHXScheduler.Skins.Terrace;
            ajanda.LoadData = true;
            ajanda.EnableDataprocessor = true;
            ajanda.InitialDate = new DateTime(2020, 4, 5);
            return View(ajanda);
        }

        public ContentResult Data()
        {
            return (new SchedulerAjaxData(
                new AjandaContext().Olays
                .Select(e => new { e.id, e.metin, e.baslangic, e.bitis })
                )
                );
        }

        public ContentResult Save(int? id, FormCollection eylemDegerleri)
        {
            var eylem = new DataAction(eylemDegerleri);
            var degismeOlayi = DHXEventsHelper.Bind<Olay>(eylemDegerleri);
            var varliklar = new AjandaContext();
            try
            {
                switch (eylem.Type)
                {
                    case DataActionTypes.Insert:
                        varliklar.Olays.Add(degismeOlayi);
                        break;
                    case DataActionTypes.Delete:
                        degismeOlayi = varliklar.Olays.FirstOrDefault(ev => ev.id == eylem.SourceId);
                        varliklar.Olays.Remove(degismeOlayi);
                        break;
                    default:// "update"
                        var target = varliklar.Olays.Single(e => e.id == degismeOlayi.id);
                        DHXEventsHelper.Update(target, degismeOlayi, new List<string> { "id" });
                        break;
                }
                varliklar.SaveChanges();
                eylem.TargetId = degismeOlayi.id;
            }
            catch (Exception a)
            {
                eylem.Type = DataActionTypes.Error;
            }

            return (new AjaxSaveResponse(eylem));
        }
    }
}