using DbScimplyAPI.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Persistence.Configurations
{
    public class SchemaConfiguration : IEntityTypeConfiguration<Schema>
    {

        public void Configure(EntityTypeBuilder<Schema> builder)
        {

            builder.HasMany(x => x.Users)
                .WithOne(x => x.Schema)
                .HasForeignKey(x => x.SchemaId);

        }

    }
}
