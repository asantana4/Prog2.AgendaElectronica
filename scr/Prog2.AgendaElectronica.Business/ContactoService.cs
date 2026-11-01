using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Prog2.AgendaElectronica.Core;
using Prog2.AgendaElectronica.Data;

namespace Prog2.AgendaElectronica.Business
{
    public class ContactoService
    {
        private readonly ContactoRepository _repositorio = new ContactoRepository();

        public void GuardarContacto(Contacto c)
        {
            if (string.IsNullOrWhiteSpace(c.Nombre) || string.IsNullOrWhiteSpace(c.Apellido))
            {
                throw new ArgumentException("El nombre y apellido son obligatorios.");
            }

            if (c.Id == 0)
            {
                _repositorio.Insertar(c);
            }
            else
            {
                _repositorio.Modificar(c);
            }
        }

        public DataTable ObtenerContactos(string filtro = "")
        {
            return _repositorio.Buscar(filtro);
        }

        public void BorrarContacto(int id)
        {
            if (id <= 0) throw new ArgumentException("Seleccione un contacto válido.");
            _repositorio.Eliminar(id);
        }
    }
}

    




