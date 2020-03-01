using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Repository.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(15);
            builder.Property(p => p.Age).HasMaxLength(5);
        }
    }
}



