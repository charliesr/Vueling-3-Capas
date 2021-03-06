﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vueling.Common.Logic.Model
{
    public class Student : IVuelingModelObject
    {

        #region Constructores
        public Student() {}

        public Student(Guid guid,int id, string nombre, string apellidos, string dni, DateTime fechaDeNacimiento, DateTime fechaDeRegistro, int edad)
        {
            Guid = guid;
            ID = id;
            Nombre = nombre;
            Apellidos = apellidos;
            DNI = dni;
            FechaDeNacimiento = fechaDeNacimiento;
            FechaHoraRegistro = fechaDeRegistro;
            Edad = edad;
        }

        public Student(string guid, string id, string nombre, string apellidos, string dni, string fechaDeNacimiento, string fechaDeRegistro, string edad)
        {
            Guid = Guid.Parse(guid);
            ID = Convert.ToInt32(id);
            Nombre = nombre;
            Apellidos = apellidos;
            DNI = dni;
            FechaDeNacimiento = DateTime.Parse(fechaDeNacimiento);
            FechaHoraRegistro = DateTime.Parse(fechaDeRegistro);
            Edad = Convert.ToInt32(edad);
        }
        #endregion

        #region Propiedades
        public Guid Guid { get; set; }
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string DNI { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public DateTime FechaHoraRegistro { get; set; }
        public int Edad { get; set; }
        #endregion

        #region Métodos
        public void SetGuid()
        {
            Guid = Guid.NewGuid();
        }
        #endregion


        public override string ToString()
        {
            return string.Concat(Guid.ToString(), ",", ID.ToString(), ",", Nombre, ",", Apellidos, ",", DNI, ",", FechaDeNacimiento.ToShortDateString(), ",", FechaHoraRegistro.ToString(), ",", Edad.ToString());
        }

        public override bool Equals(object obj)
        {
            var castedObject = (Student)obj;
            return castedObject != null &&
                castedObject.ID == ID &&
                castedObject.Nombre == Nombre &&
                castedObject.Apellidos == Apellidos &&
                castedObject.DNI == DNI &&
                castedObject.FechaDeNacimiento.Date == FechaDeNacimiento.Date &&
                castedObject.FechaHoraRegistro.Date == FechaHoraRegistro.Date &&
                castedObject.Edad == Edad &&
                castedObject.Guid == Guid;
        }

        public override int GetHashCode()
        {
            var hashCode = 287315274;
            hashCode = hashCode * -1521134295 + Guid.GetHashCode();
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nombre);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Apellidos);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DNI);
            hashCode = hashCode * -1521134295 + FechaDeNacimiento.GetHashCode();
            hashCode = hashCode * -1521134295 + FechaHoraRegistro.GetHashCode();
            hashCode = hashCode * -1521134295 + Edad.GetHashCode();
            return hashCode;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public object GetObjectWithoutId()
        {
            return new
            {
                Guid,
                Nombre,
                Apellidos,
                DNI,
                FechaDeNacimiento,
                FechaHoraRegistro,
                Edad
            };
        }
    }
}
