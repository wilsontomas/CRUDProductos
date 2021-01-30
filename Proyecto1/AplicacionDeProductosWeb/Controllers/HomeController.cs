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
        IMetodosDeCrud metodos;

        public HomeController(IMetodosDeCrud metodos)
        {
            this.metodos = metodos;
        }
        public ActionResult Index()
        {
            
            //List<ProductoCategoria> productos = new List<ProductoCategoria>();
           var productos = metodos.ObtenerProductosCategorias();
            foreach (var item in productos)
            {
                item.Suplidores = metodos.ObtenerSuplidoresPorId(item.IdProducto);
            }
            foreach(var item in productos)
            {
                item.Imagenes = metodos.ObtenerImagenesPorId(item.IdProducto);
            }
            return View(productos);
        }
        public ActionResult AgregarProducto()
        {
            ViewBag.Respuesta = "";
            
            Producto producto = new Producto();
            VMProductoCategoria VM = new VMProductoCategoria();
            //VM.categorias = metodos.ObtenerCategorias();
            VM.categorias = metodos.ObtenerCategoriasADO();
            VM.producto = producto;
            return View(VM);
        }
        [HttpPost]
        public ActionResult AgregarProducto(Producto agregado,List<HttpPostedFileBase> FotoSubida, SuplidoresSeleccionados suplidores)
        {
            ViewBag.Respuesta = "";
            

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
          
            VM.producto = metodos.BuscarProducto(Id);
            if(VM.producto != null)
            {
                VM.categorias = metodos.ObtenerCategorias();
                return View(VM);
            }
            else
            {
                return RedirectToAction("Index");
            }
           
        }
        [HttpPost]
        public ActionResult EditarProducto(int Id,Producto producto, List<HttpPostedFileBase> FotoSubida, SuplidoresSeleccionados suplidores)
        {
            
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
       
       
    }
}