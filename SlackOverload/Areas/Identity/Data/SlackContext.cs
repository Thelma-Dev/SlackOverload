using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SlackOverload.Areas.Identity.Data;
using SlackOverload.Models;

namespace SlackOverload.Data;

public class SlackContext : IdentityDbContext<ApplicationUser>
{
    public SlackContext(DbContextOptions<SlackContext> options)
        : base(options)
    {
    }

    public DbSet<Question> Question { get; set; } = default!;

    public DbSet<Answer> Answer { get; set; } = default!;

    public DbSet<Tags> Tags { get; set; } = default!;

    public DbSet<QuestionComment> QuestionComment { get; set; } = default!;

    public DbSet<AnswerComment> AnswerComment { get; set; } = default!;

    public DbSet<QuestionVote> QuestionVote { get; set; } = default!;

    public DbSet<AnswerVote> AnswerVote { get; set; } = default!;

    public DbSet<QuestionTag> QuestionTag { get; set; } = default!;

    public DbSet<MarkedAnswers> MarkedAnswer { get; set; } = default!;

    private void _createSlackOverflowModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Question>()
            .HasKey(q => q.Id);

        modelBuilder.Entity<Answer>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<Question>()
            .HasMany(q => q.Answers)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.QuestionId);

        modelBuilder.Entity<Answer>()
            .HasMany(a => a.AnswerComments)
            .WithOne(ac => ac.Answer)
            .HasForeignKey(ac => ac.AnswerId);

        modelBuilder.Entity<Answer>()
            .HasMany(a => a.AnswerVotes)
            .WithOne(av => av.Answer)
            .HasForeignKey(av => av.AnswerId);

        modelBuilder.Entity<Question>()
            .HasMany(q => q.QuestionTags)
            .WithOne(qt => qt.Question)
            .HasForeignKey(qt => qt.QuestionId);

        modelBuilder.Entity<Question>()
            .HasMany(q => q.QuestionComments)
            .WithOne(qc => qc.Question) 
            .HasForeignKey(qc => qc.QuestionId);

        modelBuilder.Entity<Question>()
            .HasMany(q => q.QuestionVotes)
            .WithOne(qc => qc.Question)
            .HasForeignKey(qc => qc.QuestionId);

        modelBuilder.Entity<Tags>()
            .HasMany(t => t.QuestionTags)
            .WithOne(t => t.Tag)
            .HasForeignKey(t => t.TagId);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        _createSlackOverflowModel(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
