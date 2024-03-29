﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gos.Usuarios {
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using Castle.ActiveRecord;
    
    
    [ActiveRecord("Perfiles", Schema="dbo")]
    public partial class Perfiles : ActiveRecordBase<Perfiles> {
        
        private int _idPerfil;
        
        private string _detalle;
        
        private IList<Usuarios> _usuarios;
        
        [PrimaryKey(PrimaryKeyType.Native, "IdPerfil", ColumnType="Int32")]
        public virtual int IdPerfil {
            get {
                return this._idPerfil;
            }
            set {
                this._idPerfil = value;
            }
        }
        
        [Property("Detalle", ColumnType="String", NotNull=true)]
        public virtual string Detalle {
            get {
                return this._detalle;
            }
            set {
                this._detalle = value;
            }
        }
        
        [HasMany(typeof(Usuarios))]
        public virtual IList<Usuarios> Usuarios {
            get {
                return this._usuarios;
            }
            set {
                this._usuarios = value;
            }
        }
    }

    [ActiveRecord("usuarios", Schema = "dbo")]
    public partial class Usuarios : ActiveRecordBase<Usuarios>
    {

        private int _idUsuario;

        private string _nombre;

        private string _loginName;

        private string _password;

        private int _idPerfil;

        private int _perfiles;

        private string _foto;


        [PrimaryKey(PrimaryKeyType.Native, "IdUsuario", ColumnType = "Int32")]
        public virtual int IdUsuario
        {
            get
            {
                return this._idUsuario;
            }
            set
            {
                this._idUsuario = value;
            }
        }

        [Property("Nombre", ColumnType = "String")]
        public virtual string Nombre
        {
            get
            {
                return this._nombre;
            }
            set
            {
                this._nombre = value;
            }
        }

        [Property("loginName", ColumnType = "String")]
        public virtual string LoginName
        {
            get
            {
                return this._loginName;
            }
            set
            {
                this._loginName = value;
            }
        }

        [Property("Password", ColumnType = "String")]
        public virtual string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
            }
        }

        [Property("IdPerfil", ColumnType = "Int32")]
        public virtual int IdPerfil
        {
            get
            {
                return this._idPerfil;
            }
            set
            {
                this._idPerfil = value;
            }
        }

        [Property("Perfiles", ColumnType = "Int32")]
        public virtual int Perfiles
        {
            get
            {
                return this._perfiles;
            }
            set
            {
                this._perfiles = value;
            }
        }

        [Property("Foto", ColumnType = "String")]
        public virtual string Foto
        {
            get
            {
                return this._foto;
            }
            set
            {
                this._foto = value;
            }
        }

    }     
}
