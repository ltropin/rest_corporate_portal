using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace restcorporate_portal.RequestModels
{
    public class RequestTaskPut
    {
        /// <summary>
        /// Идентификатор изменяемой задачи
        /// </summary>
        [Required]
        public int TaskId { get; set; }

        /// <summary>
        /// Новый статус задачи
        /// </summary>
        [Required]
        public int NewStatusId { get; set; }
    }
}
