using Microservice.Catalog.Api.Features.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection.Emit;

namespace Microservice.Catalog.Api.Repositories
{
    public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            //Collection tablo, document satır, field sütun
            builder.ToCollection("courses");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).HasElementName("name").HasMaxLength(100);
            builder.Property(x => x.Description).HasElementName("description").HasMaxLength(1000);
            builder.Property(x => x.Created).HasElementName("created");
            builder.Property(x => x.UserId).HasElementName("userId");
            builder.Property(x => x.ImageUrl).HasElementName("imageIrl").HasMaxLength(200);
            builder.Property(x => x.CategoryId).HasElementName("categoryId");
            builder.Ignore(c => c.Category);

            builder.OwnsOne(c => c.Feature, feature =>
            {
                feature.HasElementName("feature");
                feature.Property(f => f.Duration).HasElementName("duration");
                feature.Property(f => f.Rating).HasElementName("rating");
                feature.Property(f => f.EducatorFullName).HasElementName("educatorFullName").HasMaxLength(100);
            });
        }
    }
}
