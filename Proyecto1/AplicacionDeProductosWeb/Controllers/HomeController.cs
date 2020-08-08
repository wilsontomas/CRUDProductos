using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibreriaDeClases;
namespace AplicacionDeProductosWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MetodosDeCrud clases = new MetodosDeCrud();
            List<ProductoCategoria> productos = new List<ProductoCategoria>();
            productos = clases.ObtenerProductosCategorias();
            foreach (var item in productos)
            {
                item.Suplidores = clases.ObtenerSuplidoresPorId(item.IdProducto);
            }
            foreach(var item in productos)
            {
                item.Imagenes = clases.ObtenerImagenesPorId(item.IdProducto);
            }
            return View(productos);
        }
        public ActionResult AgregarProducto()
        {
            ViewBag.Respuesta = "";
            MetodosDeCrud metodos = new MetodosDeCrud();
            Producto producto = new Producto();
            VMProductoCategoria VM = new VMProductoCategoria();
            VM.categorias = metodos.ObtenerCategorias();
            VM.producto = producto;
            return View(VM);
        }
        [HttpPost]
        public ActionResult AgregarProducto(Producto agregado,List<HttpPostedFileBase> FotoSubida, SuplidoresSeleccionados suplidores)
        {
            ViewBag.Respuesta = "";
            MetodosDeCrud metodos = new MetodosDeCrud();

            if(FotoSubida.Count >0)
            {
                List<byte[]> ListaDeFotos = new List<byte[]>();
                foreach(var item in FotoSubida)
                {
                    MemoryStream target = new MemoryStream();
                    item.InputStream.CopyTo(target);
                    ListaDeFotos.Add(target.ToArray());
                   
                }
                agregado.Imagenes = ListaDeFotos;
             //MemoryStream target = new MemoryStream();
             //FotoSubida.InputStream.CopyTo(target);
             //agregado.Foto =target.ToArray();
            }
           



            if (ModelState.IsValid)
            {
               if (metodos.AgregarProducto(agregado,suplidores))
                {
                    ViewBag.Respuesta = "PRODUCTO AGREGADO";
                }
                else { ViewBag.Respuesta = "NO SE PUDO AGREGAR EL PRODUCTO"; }
            }
            else
            {
                ViewBag.Respuesta = "NO SE PUDO AGREGAR EL PRODUCTO";
            }
                  
            Producto producto = new Producto();
            VMProductoCategoria VM = new VMProductoCategoria();
            VM.categorias = metodos.ObtenerCategorias();
            VM.producto = producto;
            return View(VM);
        }
        [HttpPost]
        public ActionResult EliminarProducto(int Id)
        {
            MetodosDeCrud metodos = new MetodosDeCrud();
            if (ModelState.IsValid)
            {
               metodos.EliminarProducto(Id);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditarProducto(int Id)
        {
            VMProductoCategoria VM = new VMProductoCategoria();
            MetodosDeCrud metodos = new MetodosDeCrud();
            VM.producto = metodos.BuscarProducto(Id);
            VM.categorias = metodos.ObtenerCategorias();
            return View(VM);
        }
        [HttpPost]
        public ActionResult EditarProducto(int Id,Producto producto, List<HttpPostedFileBase> FotoSubida, SuplidoresSeleccionados suplidores)
        {
            MetodosDeCrud metodos = new MetodosDeCrud();
            if (FotoSubida.Count >0)
            {
                List<byte[]> ListaDeFotos = new List<byte[]>();
                foreach (var item in FotoSubida)
                {
                    MemoryStream target = new MemoryStream();
                    item.InputStream.CopyTo(target);
                    ListaDeFotos.Add(target.ToArray());

                }
                producto.Imagenes = ListaDeFotos;
               
            }
           

            producto.Suplidores = metodos.ObtenerListaSuplidores(suplidores, Id);
            if (ModelState.IsValid)
            {
                if (metodos.EditarProducto(producto))
                {
                  return RedirectToAction("Index");
                }
               
            }
            VMProductoCategoria VM = new VMProductoCategoria();
           
            VM.producto = metodos.BuscarProducto(Id);
           // VM.producto.Suplidores = metodos.ObtenerListaSuplidores(suplidores, Id);
            VM.categorias = metodos.ObtenerCategorias();
            return View(VM);
        }
       
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}