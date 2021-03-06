﻿using System.ComponentModel.DataAnnotations.Schema;
using LoginTestApp.DataAccess.Contracts.Entities;

namespace LoginTestApp.DataAccess.Contracts
{
    /// <summary>
    /// Base class that configures the common properties of an entity
    /// </summary>
    public abstract class EntityTypeConfigurationBaseInt<T> : EntityTypeConfigurationBase<T>
        where T : EntityBase<int>
    {
        protected EntityTypeConfigurationBaseInt(string tableName, DatabaseGeneratedOption autoGeneratedOption)
            : base(tableName)
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(autoGeneratedOption)
                .IsRequired()
                .HasColumnName("Id");
        }
    }
}
