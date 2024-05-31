﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardLibrary.Product
{
	public class CreateProductDto
	{
		public string? ProductName { get; set; }
		public string? ProductDescription { get; set; }
		public decimal Price { get; set; }
		public string? ImageURL { get; set; }
		public int CategoryId { get; set; }
	}
}
