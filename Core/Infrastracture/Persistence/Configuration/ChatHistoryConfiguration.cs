using Core.Infrastracture.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infrastracture.Persistence.Configuration
{
    public class ChatHistoryConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.ToTable("ChatMessages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Message)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.TimeStamp)
                .IsRequired();
        }
    }
}
