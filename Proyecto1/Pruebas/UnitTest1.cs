using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibreriaDeClases;
using System.Collections.Generic;

namespace Pruebas
{
    [TestClass]
    public class UnitTest1
    {
        MetodosDeCrud metodos = new MetodosDeCrud();
        [TestMethod]
        public void PruebaImagenes()
        {          
            var listaEsperada = new List<byte[]>();
            var listaActual = metodos.ObtenerImagenesPorId(8);
            Assert.AreEqual(listaEsperada.GetType(), listaActual.GetType());
        }

        [TestMethod]
        public void PruebaSuplidores()
        {          
            var listaEsperada = new List<Suplidor>();
            var listaActual = metodos.ObtenerSuplidoresPorId(8);          
            Assert.AreEqual(listaEsperada.GetType(), listaActual.GetType());        
        }

        [TestMethod]
        public void PruebaCategorias()
        {
            var listaEsperada = new List<Categoria>();
            var listaActual = metodos.ObtenerCategorias();
            Assert.AreEqual(listaEsperada.GetType(), listaActual.GetType());
        }
    }
}
