﻿using NamasStudio.Dto.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamasStudio.Dto.Category
{
    public class CategoryProductInsertDto
    {
        [UniqueCategoryName(ErrorMessage = "Category name already exists.")]
        public string CategoryName { get; set; }

        public string Description { get; set; }

        public DateTime? CreateAt { get; } = DateTime.Now;
    }

}
