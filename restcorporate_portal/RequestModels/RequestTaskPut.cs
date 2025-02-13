﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace restcorporate_portal.RequestModels
{
    public class RequestFilePut
    {
        /// <summary>
        /// Идентификатор изменяемой задачи
        /// </summary>
        [Required]
        public int TaskId { get; set; }

        /// <summary>
        /// Новый URL приложенного файла
        /// </summary>
        [Required]
        public string NewFileUrl { get; set; }
    }
}
