using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RandomPhotosAPI.Database.DatabaseModels;

namespace RandomPhotosAPI.Database.Configurations
{
    public class PhotoEntityConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasKey(c => c.ID);

            builder.Property(c => c.Url)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(c => c.Date)
               .IsRequired();
        }
    }
}
