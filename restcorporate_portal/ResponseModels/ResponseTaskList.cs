﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using restcorporate_portal.Models;

namespace restcorporate_portal.ResponseModels
{
    public class ResponseTaskList
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Column(TypeName = "date")]
        public DateTime ExpirationDate { get; set; }
        public string ShortExpirationDate { get => ExpirationDate.ToString("dd.MM.yyyy"); }
        public int RewardCoins { get; set; }
        
        public string AttachedFileUrl { get; set; }

        public bool IsExpired { get => DateTime.Now > ExpirationDate; }
        [Column(TypeName = "date")]
        public DateTime DateUpdate { get; set; }
        [Column("RewardXP")]
        public int RewardXp { get; set; }

        public virtual ResponseDifficultyList Difficulty { get; set; }

        public virtual ResponsePriorityList Priorirty { get; set; }

        public virtual ResponseStatusList Status { get; set; }

        public virtual ResponseWorkerList Worker { get; set; }
        public virtual ResponseWorkerList Author { get; set; }

        public virtual ResponseFileList Icon { get; set; }

        public static ResponseTaskList FromApiTask(Task value,
            File icon,
            Worker author,
            File avatarWorker = null,
            File avatarAuthor = null) =>
            value == null ? null :
            new ResponseTaskList
            {
                Id = value.Id,
                Title = value.Title,
                Description = value.Description,
                ExpirationDate = value.ExpirationDate,
                DateUpdate = value.DateUpdate,
                RewardCoins = value.RewardCoins,
                RewardXp = value.RewardXp,
                Difficulty = ResponseDifficultyList.FromApiDifficulty(value.Difficulty),
                Status = ResponseStatusList.FromApiStatus(value.Status),
                Priorirty = ResponsePriorityList.FromApiPriority(value.Priorirty),
                Worker = ResponseWorkerList.FromApiWorker(value.Worker, avatar: avatarWorker),
                Icon = ResponseFileList.FromApiFile(icon),
                Author = ResponseWorkerList.FromApiWorker(author, avatar: avatarAuthor),
            };
    }
}
