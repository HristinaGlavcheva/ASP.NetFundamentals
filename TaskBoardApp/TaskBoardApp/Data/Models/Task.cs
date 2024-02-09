﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TaskBoardApp.Data.DataConstants;

namespace TaskBoardApp.Data.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(TaskTitleMaxLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(TaskDescriptionMaxLength)]
        public string Descripion { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; }

        public int? BoardId { get; set; }

        public Board? Board { get; set; }

        [Required]
        public string OwnerId { get; set; } = string.Empty;

        [ForeignKey(nameof (OwnerId))]
        public IdentityUser Owner { get; set; } = null!;
    }
}
