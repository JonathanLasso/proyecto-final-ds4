using proyectoFinalDs4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace proyectoFinalDs4.Controllers
{
    public class AgendarTareasController : Controller
    {
        
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                ViewBag.lista = Agendar.todasLasTareasAgendadas();
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrió un error al cargar las tareas.";
                return View();
            }
        }

        
        [HttpGet]
        public ActionResult CrearTarea()
        {
            try
            {
                return View(new Agendar());
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearTarea(Agendar tarea)
        {
            if (!ModelState.IsValid)
            {
                return View(tarea);
            }

            try
            {
                tarea.insertarTarea();
                TempData["mensaje"] = "Tarea agendada correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al guardar la tarea.");
                return View(tarea);
            }
        }

       
        [HttpGet]
        public ActionResult EditarTarea(int id)
        {
            try
            {
                Agendar tarea = Agendar.ObtenerPorId(id);

                if (tarea == null)
                {
                    return HttpNotFound();
                }

                return View(tarea);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarTarea(Agendar tarea)
        {
            if (!ModelState.IsValid)
            {
                return View(tarea);
            }

            try
            {
                tarea.actualizarTarea();
                TempData["mensaje"] = "Tarea actualizada correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al actualizar la tarea.");
                return View(tarea);
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id)
        {
            try
            {
                Agendar tarea = new Agendar();
                tarea.eliminarTarea(id);

                TempData["mensaje"] = "Tarea eliminada correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrió un error al eliminar la tarea.";
                return RedirectToAction("Index");
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarTodas()
        {
            try
            {
                Agendar tarea = new Agendar();
                tarea.eliminarTodasLasTareas();

                TempData["mensaje"] = "Todas las tareas fueron eliminadas";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrió un error al eliminar todas las tareas.";
                return RedirectToAction("Index");
            }
        }
    }
}
