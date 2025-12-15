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
            ViewBag.lista = Agendar.todasLasTareasAgendadas();
            return View();
        }

        [HttpGet]
        public ActionResult CrearTarea()
        {
            return View(new Agendar());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearTarea(Agendar tarea)
        {
            if (!ModelState.IsValid)
            {
                return View(tarea); // vuelve a la misma vista
            }
            try
            {
                tarea.insertarTarea(); // usa tu método del modelo
                TempData["mensaje"] = "Tarea agendada correctamente";
                return RedirectToAction("Index"); // o mostrarListaTareas
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(tarea);
            }
        }
        [HttpGet]
        public ActionResult EditarTarea(int id)
        {
            Agendar modelo = Agendar.ObtenerPorId(id);
            if (modelo == null)
            {
                return HttpNotFound();
            }
            return View(modelo);
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
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al actualizar la tarea.\"");
                return View(tarea);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id)
        {
            Agendar tarea = new Agendar();
            tarea.eliminarTarea(id);

            TempData["mensaje"] = "Tarea eliminada correctamente";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult EliminarTodas()
        {
            Agendar tarea = new Agendar();
            tarea.eliminarTodasLasTareas();

            TempData["mensaje"] = "Todas las tareas fueron eliminadas";
            return RedirectToAction("Index");
        }

    }
}