// PCL

namespace PCL
{
	[StandardModule]
	public static class ModBase
	{
		public enum BuildTypes
		{
			Debug = 100,
			Release = 50,
			Snapshot = 0
		}
		public class Logo
		{
			public const string IconButtonLikeLine = "M512 896a42.666667 42.666667 0 0 1-30.293333-12.373333l-331.52-331.946667a224.426667 224.426667 0 0 1 0-315.733333 223.573333 223.573333 0 0 1 315.733333 0L512 282.026667l46.08-46.08a223.573333 223.573333 0 0 1 315.733333 0 224.426667 224.426667 0 0 1 0 315.733333l-331.52 331.946667A42.666667 42.666667 0 0 1 512 896zM308.053333 256a136.533333 136.533333 0 0 0-97.28 40.106667 138.24 138.24 0 0 0 0 194.986666L512 792.746667l301.226667-301.653334a138.24 138.24 0 0 0 0-194.986666 141.653333 141.653333 0 0 0-194.56 0l-76.373334 76.8a42.666667 42.666667 0 0 1-60.586666 0L405.333333 296.106667A136.533333 136.533333 0 0 0 308.053333 256z";
			public const string IconButtonLikeFill = "M700.856 155.543c-74.769 0-144.295 72.696-190.046 127.26-45.737-54.576-115.247-127.26-190.056-127.26-134.79 0-244.443 105.78-244.443 235.799 0 77.57 39.278 131.988 70.845 175.713C238.908 694.053 469.62 852.094 479.39 858.757c9.41 6.414 20.424 9.629 31.401 9.629 11.006 0 21.998-3.215 31.398-9.63 9.782-6.662 240.514-164.703 332.238-291.701 31.587-43.724 70.874-98.143 70.874-175.713-0.001-130.02-109.656-235.8-244.445-235.8z m0 0";
			public const string IconButtonDelete = "M520.192 0C408.43 0 317.44 82.87 313.563 186.734H52.736c-29.038 0-52.663 21.943-52.663 49.079s23.625 49.152 52.663 49.152h58.075v550.473c0 103.35 75.118 187.757 167.717 187.757h472.43c92.599 0 167.716-83.894 167.716-187.757V285.477h52.59c29.038 0 52.59-21.943 52.663-49.08-0.073-27.135-23.625-49.151-52.663-49.151H726.235C723.237 83.017 631.955 0 520.192 0zM404.846 177.957c3.803-50.03 50.176-89.015 107.447-89.015 57.197 0 103.57 38.985 106.788 89.015H404.92zM284.379 933.669c-33.353 0-69.997-39.351-69.997-95.525v-549.01H833.39v549.522c0 56.247-36.645 95.525-69.998 95.525H284.379v-0.512z M357.23 800.695a48.274 48.274 0 0 0 47.616-49.006V471.7a48.274 48.274 0 0 0-47.543-49.08 48.274 48.274 0 0 0-47.69 49.006V751.69c0 27.282 20.846 49.006 47.617 49.006z m166.62 0a48.274 48.274 0 0 0 47.688-49.006V471.7a48.274 48.274 0 0 0-47.689-49.08 48.274 48.274 0 0 0-47.543 49.006V751.69c0 27.282 21.431 49.006 47.543 49.006z m142.92 0a48.274 48.274 0 0 0 47.543-49.006V471.7a48.274 48.274 0 0 0-47.543-49.08 48.274 48.274 0 0 0-47.616 49.006V751.69c0 27.282 20.773 49.006 47.543 49.006z";
			public const string IconButtonStop = "M508 990.4c-261.6 0-474.4-212-474.4-474.4S246.4 41.6 508 41.6s474.4 212 474.4 474.4S769.6 990.4 508 990.4zM508 136.8c-209.6 0-379.2 169.6-379.2 379.2 0 209.6 169.6 379.2 379.2 379.2s379.2-169.6 379.2-379.2C887.2 306.4 717.6 136.8 508 136.8zM697.6 563.2 318.4 563.2c-26.4 0-47.2-21.6-47.2-47.2 0-26.4 21.6-47.2 47.2-47.2l379.2 0c26.4 0 47.2 21.6 47.2 47.2C744.8 542.4 724 563.2 697.6 563.2z";
			public const string IconButtonCheck = "M512 0a512 512 0 1 0 512 512A512 512 0 0 0 512 0z m0 921.6a409.6 409.6 0 1 1 409.6-409.6 409.6 409.6 0 0 1-409.6 409.6z M716.8 339.968l-256 253.44L328.192 460.8A51.2 51.2 0 0 0 256 532.992l168.448 168.96a51.2 51.2 0 0 0 72.704 0l289.28-289.792A51.2 51.2 0 0 0 716.8 339.968z";
			public const string IconButtonEdit = "M732.64 64.32C688.576 21.216 613.696 21.216 569.6 64.32L120.128 499.52c-17.6 12.896-26.432 30.144-30.848 51.68L32 870.048c0 25.856 8.8 56 26.432 73.248 17.632 17.216 17.632 48.704 88.64 48.704h13.248l326.08-56c22.016-4.32 39.68-12.928 52.864-30.176l449.472-435.2c22.048-21.536 35.264-47.36 35.264-77.536 0-30.176-13.216-56-35.264-77.568l-256.096-251.2zM139.712 903.776l56-326.912 311.04-295.136 267.104 269.44-310.976 295.168-323.168 57.44zM844.576 467.84l-273.984-260.672 61.856-59.84c8.832-8.512 26.528-8.512 39.776 0l234.24 226.496c4.384 4.288 8.832 12.8 8.832 17.088s-4.416 8.544-8.864 12.8l-61.856 64.128z";
			public const string IconButtonSetup = "M651.946667 1001.813333c-22.186667 0-42.666667-10.24-61.44-27.306666-23.893333-23.893333-49.493333-35.84-75.093334-35.84-29.013333 0-56.32 11.946667-73.386666 30.72v3.413333c-17.066667 17.066667-42.666667 27.306667-66.56 27.306667h-6.826667c-6.826667 0-11.946667-1.706667-15.36-1.706667l-6.826667-1.706667c-64.853333-20.48-121.173333-54.613333-168.96-98.986666-29.013333-23.893333-37.546667-63.146667-25.6-95.573334 8.533333-23.893333 5.12-51.2-10.24-75.093333-15.36-27.306667-34.133333-40.96-59.733333-47.786667h-1.706667l-5.12-1.706666c-35.84-8.533333-61.44-34.133333-66.56-69.973334C1.706667 575.146667 0 537.6 0 512c0-32.426667 3.413333-63.146667 8.533333-93.866667v-6.826666l3.413334-8.533334c10.24-23.893333 23.893333-40.96 44.373333-51.2 5.12-3.413333 11.946667-6.826667 20.48-8.533333 27.306667-8.533333 51.2-25.6 63.146667-44.373333 13.653333-23.893333 17.066667-52.906667 10.24-81.92-11.946667-34.133333 0-71.68 30.72-93.866667 44.373333-37.546667 97.28-68.266667 158.72-93.866667l3.413333-1.706666c44.373333-13.653333 75.093333 3.413333 92.16 20.48 23.893333 23.893333 49.493333 35.84 75.093333 35.84 30.72 0 56.32-10.24 71.68-30.72l3.413334-3.413334c27.306667-27.306667 63.146667-35.84 93.866666-22.186666 63.146667 22.186667 117.76 54.613333 165.546667 97.28 29.013333 23.893333 37.546667 63.146667 25.6 95.573333-8.533333 23.893333-5.12 51.2 10.24 75.093333 15.36 27.306667 34.133333 40.96 59.733333 47.786667h1.706667l5.12 1.706667c35.84 8.533333 61.44 34.133333 66.56 71.68 6.826667 30.72 10.24 63.146667 11.946667 93.866666v3.413334c0 32.426667-3.413333 63.146667-8.533334 93.866666v6.826667l-3.413333 8.533333c-10.24 23.893333-23.893333 40.96-44.373333 51.2-5.12 3.413333-11.946667 6.826667-20.48 8.533334-27.306667 8.533333-51.2 25.6-63.146667 46.08-13.653333 23.893333-17.066667 52.906667-10.24 81.92 11.946667 35.84-1.706667 75.093333-30.72 95.573333-44.373333 35.84-95.573333 66.56-157.013333 92.16-15.36 3.413333-27.306667 3.413333-35.84 3.413333z m3.413333-83.626666z m1.706667 0zM517.12 853.333333c47.786667 0 93.866667 20.48 134.826667 59.733334 1.706667 1.706667 3.413333 1.706667 3.413333 3.413333 52.906667-22.186667 97.28-49.493333 136.533333-80.213333l1.706667-1.706667v-3.413333c-13.653333-52.906667-8.533333-104.106667 17.066667-148.48 23.893333-39.253333 64.853333-69.973333 114.346666-85.333334 1.706667 0 3.413333-1.706667 6.826667-6.826666 5.12-25.6 8.533333-51.2 8.533333-78.506667-1.706667-29.013333-3.413333-56.32-10.24-81.92v-5.12h-1.706666c-51.2-11.946667-90.453333-39.253333-119.466667-87.04-27.306667-44.373333-34.133333-100.693333-17.066667-148.48l-1.706666-1.706667h-3.413334c-39.253333-35.84-85.333333-63.146667-136.533333-80.213333H648.533333s-1.706667 1.706667-3.413333 1.706667c-32.426667 39.253333-80.213333 59.733333-136.533333 59.733333-47.786667 0-93.866667-20.48-134.826667-59.733333l-1.706667-1.706667h-1.706666c-54.613333 22.186667-98.986667 49.493333-136.533334 80.213333l-1.706666 1.706667v3.413333c13.653333 52.906667 8.533333 104.106667-17.066667 148.48-23.893333 39.253333-64.853333 69.973333-114.346667 85.333334-1.706667 0-3.413333 1.706667-6.826666 6.826666-6.826667 25.6-8.533333 51.2-8.533334 78.506667 0 30.72 3.413333 58.026667 6.826667 76.8l1.706667 5.12h1.706666c51.2 11.946667 90.453333 39.253333 119.466667 87.04 27.306667 44.373333 34.133333 100.693333 17.066667 148.48l1.706666 1.706667 1.706667 1.706666c37.546667 35.84 83.626667 63.146667 134.826667 80.213334 1.706667 0 3.413333 0 3.413333 1.706666h1.706667s1.706667 0 5.12-1.706666c34.133333-37.546667 81.92-59.733333 136.533333-59.733334z m-6.826667-146.773333c-110.933333 0-199.68-85.333333-199.68-196.266667 0-109.226667 87.04-196.266667 199.68-196.266666s199.68 85.333333 199.68 196.266666c-1.706667 109.226667-88.746667 196.266667-199.68 196.266667z m0-307.2c-63.146667 0-114.346667 49.493333-114.346666 110.933333 0 63.146667 49.493333 110.933333 114.346666 110.933334 30.72 0 59.733333-11.946667 80.213334-32.426667 20.48-20.48 32.426667-49.493333 32.426666-78.506667 0-63.146667-49.493333-110.933333-112.64-110.933333z";
			public const string IconButtonReset = "M667.6817627 313.65283203l-45.28564454 55.76660156L858.06933594 391.27124023 787.61950684 165.93066406l-56.01379395 69.01611328A354.47387695 354.47387695 0 0 0 520.89892578 165.93066406C324.87536621 165.93066406 165.93066406 324.43041992 165.93066406 519.91015625c0 195.52917481 158.94470215 353.97949219 354.96826172 353.97949219a355.06713867 355.06713867 0 0 0 331.73217774-227.66418458 50.52612305 50.52612305 0 0 0-29.21813966-65.25878905 50.77331543 50.77331543 0 0 0-65.50598144 29.16870117A253.61938477 253.61938477 0 0 1 520.94836426 772.78796387c-140.05920411 0-253.61938477-113.21411133-253.61938477-252.87780762 0-139.61425781 113.56018067-252.82836914 253.61938477-252.82836914 53.59130859 0 104.46350098 16.61132813 146.73339843 46.57104492";
			public const string IconButtonRefresh = "M512.0 838.3c-80.2 0-153.4-29.3-210.2-77.4l75.5-75.5c11.5-11.5 25.8-22.0 25.8-37.0a27.2 27.2 0 0 0-27.1-27.1H104.0c-27.1 0-27.1 23.9-27.1 27.1v271.9a27.1 27.1 0 0 0 27.1 27.1c15.0 0 27.8-16.6 42.5-31.2l77.9-77.9c76.6 67.7 177.1 108.9 287.4 108.9 221.7 0 404.5-166.0 431.2-380.6h-109.8c-25.9 154.2-159.7 271.9-321.3 271.9zM919.9 76.6c-15.0 0-27.8 16.6-42.5 31.3L799.5 185.8c-76.5-67.7-177.1-108.9-287.4-108.9-221.8 0-404.5 166.1-431.3 380.6H190.6c25.9-154.2 159.7-271.9 321.4-271.9 80.2 0 153.4 29.3 210.1 77.4l-75.5 75.5c-11.6 11.5-25.8 22.0-25.8 37.1a27.2 27.2 0 0 0 27.1 27.1h271.9c27.1 0 27.1-23.9 27.1-27.1V103.8a27.1 27.1 0 0 0-27.1-27.1z";
			public const string IconButtonSave = "M819.392 0L1024 202.752v652.16a168.96 168.96 0 0 1-168.832 168.768h-104.192a47.296 47.296 0 0 1-10.752 0H283.776a47.232 47.232 0 0 1-10.752 0H168.832A168.96 168.96 0 0 1 0 854.912V168.768A168.96 168.96 0 0 1 168.832 0h650.56z m110.208 854.912V242.112l-149.12-147.776H168.896c-41.088 0-74.432 33.408-74.432 74.432v686.144c0 41.024 33.344 74.432 74.432 74.432h62.4v-190.528c0-33.408 27.136-60.544 60.544-60.544h440.448c33.408 0 60.544 27.136 60.544 60.544v190.528h62.4c41.088 0 74.432-33.408 74.432-74.432z m-604.032 74.432h372.864v-156.736H325.568v156.736z m403.52-596.48a47.168 47.168 0 1 1 0 94.336H287.872a47.168 47.168 0 1 1 0-94.336h441.216z m0-153.728a47.168 47.168 0 1 1 0 94.4H287.872a47.168 47.168 0 1 1 0-94.4h441.216z";
			public const string IconButtonInfo = "M512 917.333333c223.861333 0 405.333333-181.472 405.333333-405.333333S735.861333 106.666667 512 106.666667 106.666667 288.138667 106.666667 512s181.472 405.333333 405.333333 405.333333z m0 106.666667C229.226667 1024 0 794.773333 0 512S229.226667 0 512 0s512 229.226667 512 512-229.226667 512-512 512z m-32-597.333333h64a21.333333 21.333333 0 0 1 21.333333 21.333333v320a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333V448a21.333333 21.333333 0 0 1 21.333333-21.333333z m0-192h64a21.333333 21.333333 0 0 1 21.333333 21.333333v64a21.333333 21.333333 0 0 1-21.333333 21.333333h-64a21.333333 21.333333 0 0 1-21.333333-21.333333v-64a21.333333 21.333333 0 0 1 21.333333-21.333333z";
			public const string IconButtonList = "M384 128h640v128H384zM160 192m-96 0a96 96 0 1 0 192 0 96 96 0 1 0-192 0ZM384 448h640v128H384zM160 512m-96 0a96 96 0 1 0 192 0 96 96 0 1 0-192 0ZM384 768h640v128H384zM160 832m-96 0a96 96 0 1 0 192 0 96 96 0 1 0-192 0Z";
			public const string IconButtonOpen = "M889.018182 418.909091H884.363636V316.509091a93.090909 93.090909 0 0 0-99.607272-89.832727h-302.545455l-93.090909-76.334546A46.545455 46.545455 0 0 0 358.865455 139.636364H146.152727A93.090909 93.090909 0 0 0 46.545455 229.469091V837.818182a46.545455 46.545455 0 0 0 46.545454 46.545454 46.545455 46.545455 0 0 0 16.756364-3.258181 109.381818 109.381818 0 0 0 25.134545 3.258181h586.472727a85.178182 85.178182 0 0 0 87.04-63.301818l163.374546-302.545454a46.545455 46.545455 0 0 0 5.585454-21.876364A82.385455 82.385455 0 0 0 889.018182 418.909091z m-744.727273-186.181818h198.283636l93.09091 76.334545a46.545455 46.545455 0 0 0 29.323636 10.705455h319.301818a12.101818 12.101818 0 0 1 6.516364 0V418.909091H302.545455a85.178182 85.178182 0 0 0-87.04 63.301818L139.636364 622.778182V232.727273a19.549091 19.549091 0 0 1 6.516363 0z m578.094546 552.029091a27.461818 27.461818 0 0 0-2.792728 6.516363H154.530909l147.083636-272.290909a27.461818 27.461818 0 0 0 2.792728-6.981818h565.061818z";
			public const string IconButtonArrowUp = "M554 333V853h-85V333l-228 228-60-60L512 170l331 331-60 60L554 333z";
			public const string IconButtonArrowDown = "M554 691V171h-85V691L241 463l-60 60L512 854l331-331-60-60L554 691z";
			public const string IconButtonCard = "M834.5 684.1c-31.2-70.4-98.9-120.9-179.1-127.3 63.5-8.5 112.6-63 112.6-128.8 0-71.8-58.2-130-130-130s-130 58.2-130 130c0 65.9 49 120.3 112.6 128.8-80.2 6.4-148 57-179.1 127.3-8.7 19.7 6 42 27.6 42 12.1 0 22.7-7.5 27.7-18.5 24.3-53.9 78.5-91.5 141.3-91.5s117 37.6 141.3 91.5c5 11.1 15.6 18.5 27.7 18.5 21.4 0 36.1-22.3 27.4-42zM567.9 427.9c0-38.6 31.4-70 70-70s70 31.4 70 70-31.4 70-70 70-70-31.4-70-70zM460.3 347.9H216.9c-16.6 0-30 13.4-30 30s13.4 30 30 30h243.3c16.6 0 30-13.4 30-30 0.1-16.5-13.4-30-29.9-30zM367.4 459.6H216.9c-16.6 0-30 13.4-30 30s13.4 30 30 30h150.4c16.6 0 30-13.4 30-30 0.1-16.6-13.4-30-29.9-30zM297.4 571.2H217c-16.6 0-30 13.4-30 30s13.4 30 30 30h80.4c16.6 0 30-13.4 30-30 0-16.5-13.5-30-30-30zM900 236v552H124V236h776m0-60H124c-33.1 0-60 26.9-60 60v552c0 33.1 26.9 60 60 60h776c33.1 0 60-26.9 60-60V236c0-33.1-26.9-60-60-60z";
			public const string IconButtonCross = "F1 M 26.9166,22.1667L 37.9999,33.25L 49.0832,22.1668L 53.8332,26.9168L 42.7499,38L 53.8332,49.0834L 49.0833,53.8334L 37.9999,42.75L 26.9166,53.8334L 22.1666,49.0833L 33.25,38L 22.1667,26.9167L 26.9166,22.1667 Z";
			public const string IconButtonMojang = "M9.183,18.967c-0.109-2.239,1.336-5.119,3.92-4.96c3.657-0.027,7.319-0.044,10.977,0.005\tc3.712,0.214,6.596,2.759,9.652,4.533c0.181-1.697-0.197-3.717,1.237-4.982c1.899,2.091,3.143,4.894,5.677,6.334\tc1.577,0.805,2.973-0.668,4.221-1.44c1.55,2.305,2.108,5.075,2.622,7.752c0.657,3.438,0.947,6.925,1.111,10.413\tc-1.734-0.733-3.355-1.708-4.971-2.671c-1.396-4.933-5.349-8.656-9.723-11.059c-3.46-1.817-7.752-2.185-11.338-0.52\tc-3.761,1.593-6.285,5.666-5.891,9.75c0.121,4.577,3.17,8.765,7.391,10.456c7.746,3.285,16.407,2.234,24.521,1.243\tc0.454,2.315-1.511,4.527-3.695,4.889c-10.577,0.038-21.148-0.011-31.725,0.022c-2.217,0.279-4.079-1.879-3.964-4.008\tC9.167,36.141,9.216,27.556,9.183,18.967z M40.114,8.09c0.294-0.801,0.872-1.417,1.542-1.924c1.379,2.589,2.742,5.482,2.311,8.491\tc-0.332,2.54-4.42,2.507-5.052,0.163C38.349,12.527,39.209,10.178,40.114,8.09z M0,53 l0.1,0 z";
			public const string IconButtonUser = "M660.338 528.065c63.61-46.825 105.131-121.964 105.131-206.83 0-141.7-115.29-256.987-256.997-256.987-141.706 0-256.998 115.288-256.998 256.987 0 85.901 42.52 161.887 107.456 208.562-152.1 59.92-260.185 207.961-260.185 381.077 0 21.276 17.253 38.53 38.53 38.53 21.278 0 38.53-17.254 38.53-38.53 0-183.426 149.232-332.671 332.667-332.671 1.589 0 3.113-0.207 4.694-0.244 0.8 0.056 1.553 0.244 2.362 0.244 183.434 0 332.664 149.245 332.664 332.671 0 21.276 17.255 38.53 38.533 38.53 21.277 0 38.53-17.254 38.53-38.53 0-174.885-110.354-324.13-264.917-382.809z m-331.803-206.83c0-99.22 80.72-179.927 179.935-179.927s179.937 80.708 179.937 179.927c0 99.203-80.721 179.91-179.937 179.91s-179.935-80.708-179.935-179.91z";
			public const string IconButtonShield = "M511.488256 95.184408c35.310345 22.516742 95.184408 55.78011 167.34033 84.437781 75.738131 29.681159 148.405797 40.93953 191.392304 45.033483v353.615193c0 73.691154-50.662669 164.781609-136.123938 244.101949C649.65917 901.181409 558.568716 942.12094 512 942.12094c-46.568716 0-137.65917-40.93953-222.096952-119.748126C204.441779 742.54073 153.77911 651.450275 153.77911 577.247376v-353.103448c42.474763-4.093953 116.165917-15.352324 191.904048-45.545227 75.226387-30.192904 133.565217-63.456272 165.805098-83.414293M512 0c-4.093953 0-8.187906 1.535232-11.258371 3.582209l-14.84058 10.234882c-1.023488 0.511744-67.550225 47.592204-170.410794 88.531735-100.813593 39.916042-198.556722 41.963018-199.58021 41.963018l-25.075462 0.511744c-10.746627 0.511744-18.934533 8.187906-18.934533 18.422789v414.000999c0 216.97951 286.064968 446.24088 440.09995 446.24088s440.09995-229.261369 440.09995-445.729136V163.758121c0-10.234883-8.69965-18.422789-18.934533-18.422789l-24.563718-0.511744c-1.023488 0-98.766617-2.046977-199.58021-41.963018-103.372314-40.93953-170.410795-88.01999-170.922538-88.531734L523.258371 3.582209c-3.070465-2.558721-7.164418-3.582209-11.258371-3.582209z M743.308346 410.930535l-260.477761 260.477761c-15.864068 15.864068-41.963018 15.864068-57.827087 0l-144.823588-144.823588c-15.864068-15.864068-15.864068-41.963018 0-57.827087 8.187906-8.187906 18.422789-11.770115 29.169415-11.770115 10.234883 0 20.981509 4.093953 29.169416 11.770115l115.654173 115.654173L685.993003 352.591704c15.864068-15.864068 41.963018-15.864068 57.827087 0 15.352324 16.375812 15.352324 42.474763-0.511744 58.338831z";
			public const string IconButtonOffline = "M533.293176 788.841412a60.235294 60.235294 0 1 1 85.202824 85.202823l-42.616471 42.586353c-129.355294 129.385412-339.124706 129.385412-468.510117 0-129.385412-129.385412-129.385412-339.124706 0-468.510117l42.586353-42.616471a60.235294 60.235294 0 1 1 85.202823 85.202824l-42.61647 42.586352a210.823529 210.823529 0 1 0 298.164706 298.164706l42.586352-42.61647z m255.548236-255.548236l42.61647-42.586352a210.823529 210.823529 0 1 0-298.164706-298.164706l-42.586352 42.61647a60.235294 60.235294 0 1 1-85.202824-85.202823l42.616471-42.586353c129.355294-129.385412 339.124706-129.385412 468.510117 0 129.385412 129.385412 129.385412 339.124706 0 468.510117l-42.586353 42.616471a60.235294 60.235294 0 1 1-85.202823-85.202824zM192.542118 192.542118a60.235294 60.235294 0 0 1 85.202823 0l553.712941 553.712941a60.235294 60.235294 0 0 1-85.202823 85.202823L192.542118 277.744941a60.235294 60.235294 0 0 1 0-85.202823z";
			public const string IconButtonServer = "M224 160a64 64 0 0 0-64 64v576a64 64 0 0 0 64 64h576a64 64 0 0 0 64-64V224a64 64 0 0 0-64-64H224z m0 384h576v256H224v-256z m192 96v64h320v-64H416z m-128 0v64h64v-64H288zM224 224h576v256H224V224z m192 96v64h320v-64H416z m-128 0v64h64v-64H288z";
			public const string IconMusic = "M348.293565 716.53287V254.797913c0-41.672348 28.004174-78.358261 68.919652-90.37913L815.994435 40.826435c62.775652-18.610087 125.907478 26.579478 125.907478 89.933913v539.158261c8.013913 42.25113-8.94887 89.177043-47.014956 127.109565a232.848696 232.848696 0 0 1-170.785392 65.758609c-61.885217-2.938435-111.081739-33.435826-129.113043-80.050087-18.031304-46.614261-2.137043-102.177391 41.672348-145.853218a232.848696 232.848696 0 0 1 170.785391-65.80313c21.014261 1.024 40.514783 5.164522 57.878261 12.065391V233.338435c0-12.109913-10.551652-20.034783-20.569044-20.034783a24.620522 24.620522 0 0 0-5.787826 0.934957L439.785739 338.18713a19.545043 19.545043 0 0 0-14.825739 19.144348v438.984348H423.846957c11.53113 43.987478-5.164522 94.208-45.412174 134.322087a232.848696 232.848696 0 0 1-170.785392 65.758609c-61.885217-2.938435-111.081739-33.435826-129.113043-80.050087-18.031304-46.614261-2.137043-102.177391 41.672348-145.853218a232.848696 232.848696 0 0 1 170.785391-65.80313c20.791652 1.024 40.069565 5.075478 57.299478 11.842783z";
			public const string IconPlay = "M803.904 463.936a55.168 55.168 0 0 1 0 96.128l-463.616 264.448C302.848 845.888 256 819.136 256 776.448V247.616c0-42.752 46.848-69.44 84.288-48.064l463.616 264.384z";
		}
		public class MyColor
		{
			public double A;
			public double R;
			public double G;
			public double B;
			public static implicit operator MyColor(string str)
			{
				return new MyColor(str);
			}
			public static implicit operator MyColor(System.Windows.Media.Color col)
			{
				return new MyColor(col);
			}
			public static implicit operator System.Windows.Media.Color(MyColor conv)
			{
				return System.Windows.Media.Color.FromArgb(ClampToByte(conv.A), ClampToByte(conv.R), ClampToByte(conv.G), ClampToByte(conv.B));
			}
			public static implicit operator System.Drawing.Color(MyColor conv)
			{
				return System.Drawing.Color.FromArgb(ClampToByte(conv.A), ClampToByte(conv.R), ClampToByte(conv.G), ClampToByte(conv.B));
			}
			public static implicit operator MyColor(SolidColorBrush bru)
			{
				return new MyColor(bru.Color);
			}
			public static implicit operator SolidColorBrush(MyColor conv)
			{
				return new SolidColorBrush(System.Windows.Media.Color.FromArgb(ClampToByte(conv.A), ClampToByte(conv.R), ClampToByte(conv.G), ClampToByte(conv.B)));
			}
			public static implicit operator MyColor(System.Windows.Media.Brush bru)
			{
				return new MyColor(bru);
			}
			public static implicit operator System.Windows.Media.Brush(MyColor conv)
			{
				return new SolidColorBrush(System.Windows.Media.Color.FromArgb(ClampToByte(conv.A), ClampToByte(conv.R), ClampToByte(conv.G), ClampToByte(conv.B)));
			}
			public static MyColor operator +(MyColor a, MyColor b)
			{
				return new MyColor
				{
					A = a.A + b.A,
					B = a.B + b.B,
					G = a.G + b.G,
					R = a.R + b.R
				};
			}
			public static MyColor operator -(MyColor a, MyColor b)
			{
				return new MyColor
				{
					A = a.A - b.A,
					B = a.B - b.B,
					G = a.G - b.G,
					R = a.R - b.R
				};
			}
			public static MyColor operator *(MyColor a, double b)
			{
				return new MyColor
				{
					A = a.A * b,
					B = a.B * b,
					G = a.G * b,
					R = a.R * b
				};
			}
			public static MyColor operator /(MyColor a, double b)
			{
				return new MyColor
				{
					A = a.A / b,
					B = a.B / b,
					G = a.G / b,
					R = a.R / b
				};
			}
			public static bool operator ==(MyColor a, MyColor b)
			{
				if (Information.IsNothing(a) && Information.IsNothing(b))
				{
					return true;
				}
				if (Information.IsNothing(a) || Information.IsNothing(b))
				{
					return false;
				}
				return a.A == b.A && a.R == b.R && a.G == b.G && a.B == b.B;
			}
			public static bool operator !=(MyColor a, MyColor b)
			{
				if (Information.IsNothing(a) && Information.IsNothing(b))
				{
					return false;
				}
				if (Information.IsNothing(a) || Information.IsNothing(b))
				{
					return true;
				}
				return a.A != b.A || a.R != b.R || a.G != b.G || a.B != b.B;
			}
			public static MyColor Lerp(MyColor ValueA, MyColor ValueB, double Percent)
			{
				return Round(ValueA * (1.0 - Percent) + ValueB * Percent, 6);
			}
			public static MyColor Round(MyColor col, int w = 0)
			{
				return new MyColor
				{
					A = Math.Round(col.A, w),
					R = Math.Round(col.R, w),
					G = Math.Round(col.G, w),
					B = Math.Round(col.B, w)
				};
			}
			public static byte ClampToByte(double d)
			{
				if (d < 0.0)
				{
					d = 0.0;
				}
				if (d > 255.0)
				{
					d = 255.0;
				}
				return checked((byte)Math.Round(d));
			}
			public MyColor()
			{
				A = 255.0;
				R = 0.0;
				G = 0.0;
				B = 0.0;
			}
			public MyColor(System.Windows.Media.Color col)
			{
				A = 255.0;
				R = 0.0;
				G = 0.0;
				B = 0.0;
				A = (int)col.A;
				R = (int)col.R;
				G = (int)col.G;
				B = (int)col.B;
			}
			public MyColor(string HexString)
			{
				A = 255.0;
				R = 0.0;
				G = 0.0;
				B = 0.0;
				object obj = System.Windows.Media.ColorConverter.ConvertFromString(HexString);
				System.Windows.Media.Color color = ((obj != null) ? ((System.Windows.Media.Color)obj) : default(System.Windows.Media.Color));
				A = (int)color.A;
				R = (int)color.R;
				G = (int)color.G;
				B = (int)color.B;
			}
			public MyColor(double newA, MyColor col)
			{
				A = 255.0;
				R = 0.0;
				G = 0.0;
				B = 0.0;
				A = newA;
				R = col.R;
				G = col.G;
				B = col.B;
			}
			public MyColor(double newR, double newG, double newB)
			{
				A = 255.0;
				R = 0.0;
				G = 0.0;
				B = 0.0;
				A = 255.0;
				R = newR;
				G = newG;
				B = newB;
			}
			public MyColor(double newA, double newR, double newG, double newB)
			{
				A = 255.0;
				R = 0.0;
				G = 0.0;
				B = 0.0;
				A = newA;
				R = newR;
				G = newG;
				B = newB;
			}
			public MyColor(System.Windows.Media.Brush brush)
			{
				A = 255.0;
				R = 0.0;
				G = 0.0;
				B = 0.0;
				System.Windows.Media.Color color = ((SolidColorBrush)brush).Color;
				A = (int)color.A;
				R = (int)color.R;
				G = (int)color.G;
				B = (int)color.B;
			}
			public MyColor(SolidColorBrush brush)
			{
				A = 255.0;
				R = 0.0;
				G = 0.0;
				B = 0.0;
				System.Windows.Media.Color color = brush.Color;
				A = (int)color.A;
				R = (int)color.R;
				G = (int)color.G;
				B = (int)color.B;
			}
			public MyColor(object obj)
			{
				A = 255.0;
				R = 0.0;
				G = 0.0;
				B = 0.0;
				if (obj == null)
				{
					A = 255.0;
					R = 255.0;
					G = 255.0;
					B = 255.0;
				}
				else if (obj is SolidColorBrush)
				{
					System.Windows.Media.Color color = ((SolidColorBrush)obj).Color;
					A = (int)color.A;
					R = (int)color.R;
					G = (int)color.G;
					B = (int)color.B;
				}
				else
				{
					A = Conversions.ToDouble(NewLateBinding.LateGet(obj, null, "A", new object[0], null, null, null));
					R = Conversions.ToDouble(NewLateBinding.LateGet(obj, null, "R", new object[0], null, null, null));
					G = Conversions.ToDouble(NewLateBinding.LateGet(obj, null, "G", new object[0], null, null, null));
					B = Conversions.ToDouble(NewLateBinding.LateGet(obj, null, "B", new object[0], null, null, null));
				}
			}
			public double Hue(double v1, double v2, double vH)
			{
				if (vH < 0.0)
				{
					vH++;
				}
				if (vH > 1.0)
				{
					vH--;
				}
				if (vH < 0.16667)
				{
					return v1 + (v2 - v1) * 6.0 * vH;
				}
				if (vH < 0.5)
				{
					return v2;
				}
				if (vH < 0.66667)
				{
					return v1 + (v2 - v1) * (4.0 - vH * 6.0);
				}
				return v1;
			}
			public MyColor FromHSL(double sH, double sS, double sL)
			{
				A = 255.0;
				if (sS == 0.0)
				{
					R = sL * 2.55;
					G = R;
					B = R;
					return this;
				}
				double num = sH / 360.0;
				double num2 = sS / 100.0;
				double num3 = sL / 100.0;
				num2 = ((num3 < 0.5) ? (num2 * num3 + num3) : (num2 * (1.0 - num3) + num3));
				num3 = 2.0 * num3 - num2;
				R = 255.0 * Hue(num3, num2, num + 1.0 / 3.0);
				G = 255.0 * Hue(num3, num2, num);
				B = 255.0 * Hue(num3, num2, num - 1.0 / 3.0);
				return this;
			}
			public MyColor FromHSL2(double sH, double sS, double sL)
			{
				A = 255.0;
				if (sS == 0.0)
				{
					R = sL * 2.55;
					G = R;
					B = R;
					return this;
				}
				sH = (sH + 3600000.0) % 360.0;
				double[] array = new double[14]
				{
					0.1, -0.06, -0.3, -0.19, -0.15, -0.24, -0.32, -0.09, 0.18, 0.05,
					-0.12, -0.02, 0.1, -0.06
				};
				double num = sH / 30.0;
				checked
				{
					int num2 = (int)Math.Floor(num);
					num = 50.0 - ((1.0 - num + (double)num2) * array[num2] + (num - (double)num2) * array[num2 + 1]) * sS;
					sL = ((sL < num) ? (sL / num) : (1.0 + (sL - num) / (100.0 - num))) * 50.0;
					FromHSL(sH, sS, sL);
					return this;
				}
			}
			public override string ToString()
			{
				return "(" + Conversions.ToString(A) + "," + Conversions.ToString(R) + "," + Conversions.ToString(G) + "," + Conversions.ToString(B) + ")";
			}
			public override bool Equals(object obj)
			{
				return this == (MyColor)obj;
			}
		}
		public class MyRect
		{
			public double Width { get; set; }
			public double Height { get; set; }
			public double Left { get; set; }
			public double Top { get; set; }
			public MyRect()
			{
				Width = 0.0;
				Height = 0.0;
				Left = 0.0;
				Top = 0.0;
			}
			public MyRect(double left, double top, double width, double height)
			{
				Width = 0.0;
				Height = 0.0;
				Left = 0.0;
				Top = 0.0;
				Left = left;
				Top = top;
				Width = width;
				Height = height;
			}
		}
		public enum LoadState
		{
			Waiting,
			Loading,
			Finished,
			Failed,
			Canceled
		}
		public enum ProcessReturnValues
		{
			Success,
			Fail,
			Exception,
			Timeout,
			Cancel,
			TaskDone
		}
		public class SearchEntry<T>
		{
			public T Item;
			public List<SearchSource> SearchSource;
			public double Similarity;
			public bool AbsoluteRight;
			public override string ToString()
			{
				return Conversions.ToString(Math.Round(Similarity, 3)) + " - " + Item.ToString();
			}
		}
		public class SearchSource
		{
			public string[] Aliases;
			public double Weight;
			public SearchSource(string[] Aliases, double Weight = 1.0)
			{
				this.Aliases = Aliases;
				this.Weight = Weight;
			}
			public SearchSource(string Text, double Weight = 1.0)
			{
				Aliases = new string[1] { Text };
				this.Weight = Weight;
			}
		}
		public class RestartException : Exception
		{
		}
		public sealed class RouteEventArgs : EventArgs
		{
			public bool RaiseByMouse;
			public bool Handled;
			public RouteEventArgs(bool RaiseByMouse = false)
			{
				Handled = false;
				this.RaiseByMouse = RaiseByMouse;
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___39_0
		{
			public string _VB_Local_Query;
			public Func<string, double> _I0;
			public _Closure___39_0(_Closure___39_0 arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Query = arg0._VB_Local_Query;
				}
			}
			[SpecialName]
			internal double _Lambda___0(string a)
			{
				return SearchSimilarity(a, _VB_Local_Query);
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___42_0<_CLS0>
		{
			public SearchEntry<_CLS0> _VB_Local_Entry;
			public _Closure___42_0(_Closure___42_0<_CLS0> arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_Entry = arg0._VB_Local_Entry;
				}
			}
			[SpecialName]
			internal bool _Lambda___0(string QueryPart)
			{
				_Closure___42_1<_CLS0> arg = default;
				_Closure___42_1<_CLS0> CS_8__locals2 = new _Closure___42_1<_CLS0>(arg);
				CS_8__locals2._VB_Local_QueryPart = QueryPart;
				return _VB_Local_Entry.SearchSource.Any([SpecialName] (SearchSource Source) => Source.Aliases.Any([SpecialName] (string Alias) => Alias.Replace(" ", "").ContainsIgnoreCase(CS_8__locals2._VB_Local_QueryPart)));
			}
		}
		[CompilerGenerated]
		internal sealed class _Closure___42_1<_CLS0>
		{
			public string _VB_Local_QueryPart;
			public Func<string, bool> _I2;
			public _Closure___42_1(_Closure___42_1<_CLS0> arg0)
			{
				if (arg0 != null)
				{
					_VB_Local_QueryPart = arg0._VB_Local_QueryPart;
				}
			}
			[SpecialName]
			internal bool _Lambda___1(SearchSource Source)
			{
				return Source.Aliases.Any([SpecialName] (string Alias) => Alias.Replace(" ", "").ContainsIgnoreCase(_VB_Local_QueryPart));
			}
			[SpecialName]
			internal bool _Lambda___2(string Alias)
			{
				return Alias.Replace(" ", "").ContainsIgnoreCase(_VB_Local_QueryPart);
			}
		}
		public const string VersionBaseName = "2.13.1.1";
		public const string CommitHash = "";
		public const int VersionCode = 410;
		public const string VersionDisplay = "快照版 2.13.1.1";
		public const string BuildTypeDisplay = "快照版";
		public const BuildTypes BuildType = BuildTypes.Snapshot;
		public static IntPtr Handle;
		public static string PathExe = Paths.Base + AppDomain.CurrentDomain.SetupInformation.ApplicationName;
		public static string PathImage = "pack://application:,,,/Plain Craft Launcher 2;component/Images/";
		public static string PathTemp = ((Operators.CompareString(Settings.Get<string>("SystemSystemCache"), "", TextCompare: false) == 0) ? (System.IO.Path.GetTempPath() + "PCL\\") : Settings.Get<string>("SystemSystemCache")).ToString().Replace("/", "\\").TrimEnd('\\') + "\\";
		public static string Lang = "zh_CN";
		public static long ApplicationStartTick = GetTimeMs();
		public static DateTime ApplicationOpenTime = DateTime.Now;
		public static string Identify = ModSecret.GetIdentify();
		public static bool IsProgramEnding = false;
		public static bool IsGBKEncoding = Encoding.Default.CodePage == 936;
		public static string OsDrive = Conversions.ToString((from p in Environment.GetLogicalDrives()
			where Directory.Exists(p)
			select p).First().Upper().First()) + ":\\";
		private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, string>> IniCache = new ConcurrentDictionary<string, ConcurrentDictionary<string, string>>();
		public static char vbLQ = Convert.ToChar(8220);
		public static char vbRQ = Convert.ToChar(8221);
		public static Lazy<string> PathPure = new Lazy<string>([SpecialName] () =>
		{
			if (Paths.Base.IsAsciiOnly())
			{
				return Paths.Base + "PCL\\";
			}
			if (Paths.AppDataThenName.IsAsciiOnly())
			{
				return Paths.AppDataThenName;
			}
			return PathTemp.IsAsciiOnly() ? PathTemp : (OsDrive + "ProgramData\\PCL\\");
		});
		private static int Uuid = 1;
		private static object UuidLock;
		public static readonly int DPI = checked((int)Math.Round(Graphics.FromHwnd(IntPtr.Zero).DpiX));
		private static readonly int UiThreadId = Thread.CurrentThread.ManagedThreadId;
		private static readonly Random Random = new Random();
		public static bool ModeDebug => Settings.Get<bool>("SystemDebugMode");
		public static void IniClearCache(string FileName)
		{
			if (!FileName.Contains(":\\"))
			{
				FileName = $"{Paths.Base}PCL\\{FileName}.ini";
			}
			IniCache.Remove(FileName);
		}
		private static ConcurrentDictionary<string, string> IniGetContent(string FileName)
		{
			ConcurrentDictionary<string, string> result;
			try
			{
				if (!FileName.Contains(":\\"))
				{
					FileName = $"{Paths.Base}PCL\\{FileName}.ini";
				}
				ConcurrentDictionary<string, string> value = null;
				if (IniCache.TryGetValue(FileName, out value))
				{
					result = value;
				}
				else if (!FileUtils.Exists(FileName))
				{
					result = null;
				}
				else
				{
					ConcurrentDictionary<string, string> concurrentDictionary = new ConcurrentDictionary<string, string>();
					string[] array = FileUtils.ReadAsLines(FileName, skipEmptyLines: true);
					foreach (string text in array)
					{
						int num = text.IndexOfF(":");
						if (num > 0)
						{
							concurrentDictionary[text.Substring(0, num)] = text.Substring(checked(num + 1));
						}
					}
					IniCache[FileName] = concurrentDictionary;
					result = concurrentDictionary;
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, $"生成 ini 文件缓存失败（{FileName}）", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModBase.vb");
				result = null;
				ProjectData.ClearProjectError();
			}
			return result;
		}
		public static string ReadIni(string FileName, string Key, string DefaultValue = "")
		{
			ConcurrentDictionary<string, string> concurrentDictionary = IniGetContent(FileName);
			if (concurrentDictionary == null)
			{
				return DefaultValue;
			}
			return concurrentDictionary.GetOrDefault(Key, DefaultValue);
		}
		public static bool HasIniKey(string FileName, string Key)
		{
			return IniGetContent(FileName)?.ContainsKey(Key) ?? false;
		}
		public static void DeleteIniKey(string FileName, string Key)
		{
			WriteIni(FileName, Key, null);
		}
		public static void WriteIni(string FileName, string Key, string Value)
		{
			try
			{
				if (Key.Contains(":"))
				{
					throw new Exception($"尝试写入 ini 文件 {FileName} 的键名中包含了冒号：{Key}");
				}
				Key = Key.ReplaceLineEndings("");
				Value = Value?.ReplaceLineEndings("");
				ConcurrentDictionary<string, string> concurrentDictionary = IniGetContent(FileName);
				if (concurrentDictionary == null)
				{
					concurrentDictionary = new ConcurrentDictionary<string, string>();
				}
				if (Value == null)
				{
					if (!concurrentDictionary.Remove(Key))
					{
						return;
					}
				}
				else
				{
					string value = null;
					if (concurrentDictionary.TryGetValue(Key, out value) && Operators.CompareString(value, Value, TextCompare: false) == 0)
					{
						return;
					}
					concurrentDictionary[Key] = Value;
				}
				StringBuilder stringBuilder = new StringBuilder();
				foreach (KeyValuePair<string, string> item in concurrentDictionary)
				{
					stringBuilder.Append(item.Key);
					stringBuilder.Append(":");
					stringBuilder.Append(item.Value);
					stringBuilder.Append("\r\n");
				}
				if (!FileName.Contains(":\\"))
				{
					FileName = $"{Paths.Base}PCL\\{FileName}.ini";
				}
				FileName = (FileName.Contains(":\\") ? FileName : (Paths.Base + FileName));
				FileUtils.Write(FileName, stringBuilder.ToString());
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, $"写入文件失败（{FileName} → {Key}:{Value}）", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModBase.vb");
				ProjectData.ClearProjectError();
			}
		}
		public static bool CheckPermission(string Folder)
		{
			bool result;
			try
			{
				if (string.IsNullOrEmpty(Folder))
				{
					result = false;
				}
				else
				{
					Folder = PathUtils.AddSlashSuffix(Folder);
					if (Folder.EndsWithF(":\\System Volume Information\\") || Folder.EndsWithF(":\\$RECYCLE.BIN\\"))
					{
						result = false;
					}
					else if (!DirectoryUtils.Exists(Folder))
					{
						result = false;
					}
					else
					{
						string filePath = $"{Folder}CheckPermission{GetUuid()}.txt";
						FileUtils.Write(filePath, "临时文件，用于检测该文件夹的权限是否正常。");
						FileUtils.Delete(filePath);
						result = true;
					}
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, $"没有对文件夹 {Folder} 的权限，请尝试以管理员权限运行 PCL", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModBase.vb");
				result = false;
				ProjectData.ClearProjectError();
			}
			return result;
		}
		public static void CheckPermissionWithException(string Folder)
		{
			if (string.IsNullOrWhiteSpace(Folder))
			{
				throw new ArgumentNullException("文件夹名不能为空！");
			}
			Folder = PathUtils.AddSlashSuffix(Folder);
			if (Folder.EndsWithF(":\\System Volume Information\\") || Folder.EndsWithF(":\\$RECYCLE.BIN\\"))
			{
				throw new UnauthorizedAccessException("没有对系统文件夹的权限！");
			}
			if (!DirectoryUtils.Exists(Folder))
			{
				throw new DirectoryNotFoundException("文件夹不存在！");
			}
			string filePath = $"{Folder}CheckPermission{GetUuid()}.txt";
			FileUtils.Write(filePath, "临时文件，用于检测该文件夹的权限是否正常。");
			FileUtils.Delete(filePath);
		}
		public static double Val(object Str)
		{
			double result;
			if (Str == null)
			{
				result = 0.0;
			}
			else
			{
				try
				{
					result = ((Str is string && Operators.ConditionalCompareObjectEqual(Str, "&", TextCompare: false)) ? 0.0 : Conversion.Val(RuntimeHelpers.GetObjectValue(Str)));
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					result = 0.0;
					ProjectData.ClearProjectError();
				}
			}
			return result;
		}
		private static double SearchSimilarity(string Source, string Query)
		{
			if (string.IsNullOrEmpty(Source) || string.IsNullOrEmpty(Query))
			{
				return 0.0;
			}
			int i = 0;
			double num = 0.0;
			StringBuilder stringBuilder = new StringBuilder(Source.Length);
			stringBuilder.Append(Source.Lower().Replace(" ", ""));
			Query = Query.Lower().Replace(" ", "");
			int length = stringBuilder.Length;
			int length2 = Query.Length;
			if (length2 == 0)
			{
				return 0.0;
			}
			checked
			{
				int num2;
				for (; i < length2; i += ((num2 <= 0) ? 1 : num2))
				{
					int j = 0;
					num2 = 0;
					int num3 = 0;
					int k;
					for (int length3 = stringBuilder.Length; j < length3; j += ((k <= 0) ? 1 : k))
					{
						for (k = 0; i + k < length2 && j + k < length3 && stringBuilder[j + k] == Query[i + k]; k++)
						{
						}
						if (k > num2)
						{
							num2 = k;
							num3 = j;
						}
					}
					if (num2 > 0)
					{
						stringBuilder.Remove(num3, num2);
						double num4 = Math.Pow(1.4, 3 + num2) - 3.6;
						num4 *= 1.0 + 0.3 * (double)Math.Max(0, 3 - Math.Abs(i - num3));
						num += num4;
					}
				}
				return num / (double)length2 * (3.0 / Math.Sqrt(length + 15)) * (double)((length2 > 2) ? 1 : (3 - length2));
			}
		}
		private static double SearchSimilarityWeighted(List<SearchSource> Source, string Query)
		{
			_Closure___39_0 arg = default;
			_Closure___39_0 CS_8__locals2 = new _Closure___39_0(arg);
			CS_8__locals2._VB_Local_Query = Query;
			double num = 0.0;
			double num2 = 0.0;
			foreach (SearchSource item in Source)
			{
				if (item.Aliases.Any())
				{
					num2 += item.Aliases.Max([SpecialName] (string a) => SearchSimilarity(a, CS_8__locals2._VB_Local_Query)) * item.Weight;
				}
				num += item.Weight;
			}
			return num2 / num;
		}
		public static List<SearchEntry<T>> Search<T>(List<SearchEntry<T>> Entries, string Query, int MaxBlurCount = 5, double MinBlurSimilarity = 0.1)
		{
			List<SearchEntry<T>> list = new List<SearchEntry<T>>();
			if (!Entries.Any())
			{
				return list;
			}
			string[] source = Query.Split(" ", removeEmptyEntries: true);
			List<SearchEntry<T>> list2 = new List<SearchEntry<T>>();
			using (List<SearchEntry<T>>.Enumerator enumerator = Entries.GetEnumerator())
			{
				_Closure___42_0<T> obj = default;
				while (enumerator.MoveNext())
				{
					obj = new _Closure___42_0<T>(obj);
					obj._VB_Local_Entry = enumerator.Current;
					obj._VB_Local_Entry.Similarity = SearchSimilarityWeighted(obj._VB_Local_Entry.SearchSource, Query);
					obj._VB_Local_Entry.AbsoluteRight = source.All(obj._Lambda___0);
					if (obj._VB_Local_Entry.AbsoluteRight || obj._VB_Local_Entry.Similarity >= MinBlurSimilarity)
					{
						list2.Add(obj._VB_Local_Entry);
					}
				}
			}
			list2 = SortByComparison(list2, [SpecialName] (SearchEntry<T> Left, SearchEntry<T> Right) => (Left.AbsoluteRight == Right.AbsoluteRight) ? (Left.Similarity > Right.Similarity) : Left.AbsoluteRight);
			int num = 0;
			foreach (SearchEntry<T> item in list2)
			{
				if (item.AbsoluteRight)
				{
					list.Add(item);
					continue;
				}
				if (num == MaxBlurCount)
				{
					break;
				}
				list.Add(item);
				num = checked(num + 1);
			}
			return list;
		}
		public static T GetResultWithTimeout<T>(this Task<T> TargetTask, CancellationTokenSource TokenSource, int TimeoutMs)
		{
			Task task = Task.Delay(TimeoutMs);
			if (Task.WhenAny(TargetTask, task).ConfigureAwait(continueOnCapturedContext: false).GetAwaiter()
				.GetResult() == task)
			{
				TokenSource.Cancel();
				throw new TimeoutException($"任务超时（{TimeoutMs} ms）");
			}
			return TargetTask.Run();
		}
		public static void GetResultWithTimeout(this Task TargetTask, CancellationTokenSource TokenSource, int TimeoutMs)
		{
			Task task = Task.Delay(TimeoutMs);
			if (Task.WhenAny(TargetTask, task).ConfigureAwait(continueOnCapturedContext: false).GetAwaiter()
				.GetResult() == task)
			{
				TokenSource.Cancel();
				throw new TimeoutException($"任务超时（{TimeoutMs} ms）");
			}
			TargetTask.Run();
		}
		public static int RunAsAdmin(string Argument)
		{
			Process process = StartProcess(new ProcessStartInfo(PathExe)
			{
				Verb = "runas",
				Arguments = Argument
			});
			process.WaitForExit();
			return process.ExitCode;
		}
		public static int GetUuid()
		{
			if (UuidLock == null)
			{
				UuidLock = RuntimeHelpers.GetObjectValue(new object());
			}
			object uuidLock = UuidLock;
			ObjectFlowControl.CheckForSyncLockOnValueType(uuidLock);
			bool lockTaken = false;
			checked
			{
				try
				{
					Monitor.Enter(uuidLock, ref lockTaken);
					Uuid++;
					return Uuid;
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(uuidLock);
					}
				}
			}
		}
		public static long GetTimeMs()
		{
			return Stopwatch.GetTimestamp() / (Stopwatch.Frequency / 1000);
		}
		public static long GetUnixTimestampUtc()
		{
			return checked((long)Math.Round((double)(DateAndTime.Now.ToUniversalTime().Ticks - 621355968000000000L) / 10000000.0));
		}
		public static DateTime GetDate(int timeStamp)
		{
			return DateTimeOffset.FromUnixTimeSeconds(timeStamp).LocalDateTime;
		}
		public static DateTime GetLocalTime(DateTime UtcDate)
		{
			return DateTime.SpecifyKind(UtcDate, DateTimeKind.Utc).ToLocalTime();
		}
		public static Process StartProcess(string FileName, string Arguments = "")
		{
			return StartProcess(new ProcessStartInfo
			{
				FileName = FileName,
				Arguments = Arguments
			});
		}
		public static Process StartProcess(ProcessStartInfo StartInfo)
		{
			StartInfo.FileName = PathUtils.ToShortPath(StartInfo.FileName);
			if (!string.IsNullOrEmpty(StartInfo.WorkingDirectory))
			{
				StartInfo.WorkingDirectory = PathUtils.ToShortPath(StartInfo.WorkingDirectory);
			}
			Logger.Info($"启动进程：{StartInfo.FileName} {StartInfo.Arguments}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModBase.vb");
			return Process.Start(StartInfo);
		}
		public static Thread RunInNewThread(Action Action, string Name = null, ThreadPriority Priority = ThreadPriority.Normal)
		{
			Thread thread = new Thread([SpecialName] () =>
			{
				try
				{
					Action();
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, $"{Name}：线程执行失败", LogBehavior.AlertThenFeedback, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModBase.vb");
					ProjectData.ClearProjectError();
				}
			});
			thread.Name = Name ?? ("Runtime New Invoke " + Conversions.ToString(GetUuid()) + "#");
			thread.Priority = Priority;
			thread.Start();
			return thread;
		}
		public static Output RunInUiWait<Output>(Func<Output> Action)
		{
			if (RunInUi())
			{
				return Action();
			}
			return System.Windows.Application.Current.Dispatcher.Invoke(Action);
		}
		public static void RunInUiWait(Action Action)
		{
			if (RunInUi())
			{
				Action();
			}
			else
			{
				System.Windows.Application.Current.Dispatcher.Invoke(Action);
			}
		}
		public static void RunInUi(Action Action, bool ForceWaitUntilLoaded = false)
		{
			if (ForceWaitUntilLoaded)
			{
				System.Windows.Application.Current.Dispatcher.InvokeAsync(Action, DispatcherPriority.Loaded);
			}
			else if (RunInUi())
			{
				Action();
			}
			else
			{
				System.Windows.Application.Current.Dispatcher.InvokeAsync(Action);
			}
		}
		public static void RunInThread(Action Action)
		{
			if (RunInUi())
			{
				RunInNewThread(Action, "Invoke " + Conversions.ToString(GetUuid()));
			}
			else
			{
				Action();
			}
		}
		public static List<T> SortByComparison<T>(this IList<T> List, Func<T, T, bool> SortRule)
		{
			List<T> list = new List<T>();
			checked
			{
				while (List.Any())
				{
					T val = List[0];
					int num = List.Count - 1;
					for (int i = 1; i <= num; i++)
					{
						if (SortRule(List[i], val))
						{
							val = List[i];
						}
					}
					List.Remove(val);
					list.Add(val);
				}
				return list;
			}
		}
		public static object GetProgramArgument(string Name, object DefaultValue = null)
		{
			string[] array = Interaction.Command().Split(" ");
			checked
			{
				int num = array.Length - 1;
				for (int i = 0; i <= num; i++)
				{
					if (Operators.CompareString(array[i], "-" + Name, TextCompare: false) == 0)
					{
						if (array.Length == i + 1 || array[i + 1].StartsWithF("-"))
						{
							return true;
						}
						return array[i + 1];
					}
				}
				return DefaultValue ?? "";
			}
		}
		public static void OpenWebsite(string Url)
		{
			try
			{
				if (!Url.StartsWithF("http", ignoreCase: true) && !Url.StartsWithF("minecraft://", ignoreCase: true) && !Url.StartsWithF("minecraft-preview://", ignoreCase: true))
				{
					throw new Exception(Url + " 不是一个有效的网址，它必须以 http 开头！");
				}
				Logger.Info($"正在打开网页：{Url}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModBase.vb");
				StartProcess(Url);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Warn(ex2, $"无法打开网页（{Url}）", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModBase.vb");
				ClipboardSet(Url, ShowSuccessHint: false);
				ModMain.MyMsgBox("可能由于浏览器未正确配置，PCL 无法为你打开网页。\r\n网址已经复制到剪贴板，若有需要可以手动粘贴访问。\r\n" + $"网址：{Url}", "无法打开网页");
				ProjectData.ClearProjectError();
			}
		}
		public static void OpenExplorer(string Location)
		{
			try
			{
				Location = Location.Replace("/", "\\").Trim(' ', '"');
				Logger.Info($"正在打开资源管理器：{Location}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModBase.vb");
				if (Location.EndsWithF("\\"))
				{
					StartProcess(Location);
				}
				else
				{
					StartProcess("explorer", $"/select,\"{Location}\"");
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Logger.Error(ex2, "打开资源管理器失败，请尝试关闭安全软件（如 360 安全卫士）", LogBehavior.Alert, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModBase.vb");
				ProjectData.ClearProjectError();
			}
		}
		public static void ClipboardSet(string Text, bool ShowSuccessHint = true)
		{
			RunInThread([SpecialName] () =>
			{
				try
				{
					Retrier.Attempt([SpecialName] (int a0) =>
					{
						((VB_AnonymousDelegate_10)([SpecialName] () =>
						{
							RunInUi([SpecialName] () =>
							{
								MyWpfExtension.Computer.Clipboard.Clear();
								if (!string.IsNullOrEmpty(Text))
								{
									MyWpfExtension.Computer.Clipboard.SetText(Text);
								}
							});
						}))();
					}, 4u, [SpecialName] (int Attempt) => TimeSpan.FromMilliseconds(200.0), [SpecialName] (Exception ex3) => true);
					if (ShowSuccessHint)
					{
						ModMain.Hint("已成功复制！", ModMain.HintType.Green);
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Error(ex2, "可能由于剪贴板被其他程序占用，文本复制失败", LogBehavior.Toast, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModBase.vb");
					ProjectData.ClearProjectError();
				}
			});
		}
		public static string ClipboardGetText()
		{
			string result = null;
			RunInUiWait([SpecialName] () =>
			{
				try
				{
					if (MyWpfExtension.Computer.Clipboard.ContainsText())
					{
						result = MyWpfExtension.Computer.Clipboard.GetText();
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					Logger.Warn(ex2, "获取剪贴板文本失败", LogBehavior.ToastIfDebug, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModBase.vb");
					ProjectData.ClearProjectError();
				}
			});
			return result;
		}
		public static void ExtractResources(string FilePath, string ResourceName)
		{
			object objectValue = RuntimeHelpers.GetObjectValue(Resources.ResourceManager.GetObject(ResourceName));
			if (objectValue is byte[])
			{
				byte[] array = (byte[])objectValue;
				if (!FileUtils.Exists(FilePath) || Operators.CompareString(CryptographyUtils.ComputeFileHash(FilePath), CryptographyUtils.ComputeHash(array), TextCompare: false) != 0)
				{
					Logger.Info($"将资源写入到文件：{ResourceName} → {FilePath}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModBase.vb");
					FileUtils.Write(FilePath, array);
				}
				return;
			}
			if (objectValue is string)
			{
				string text = (string)objectValue;
				if (!FileUtils.Exists(FilePath) || Operators.CompareString(FileUtils.ReadAsString(FilePath), text, TextCompare: false) != 0)
				{
					Logger.Info($"将资源写入到文件：{ResourceName} → {FilePath}", LogBehavior.None, "E:\\Projects\\PCL\\本体代码\\Plain Craft Launcher 2\\Modules\\Base\\ModBase.vb");
					FileUtils.Write(FilePath, text);
				}
				return;
			}
			throw new Exception($"资源 {ResourceName} 的类型不支持：{objectValue.GetType().Name}");
		}
		public static double GetPixelSize(double WPFSize)
		{
			return WPFSize / 96.0 * (double)DPI;
		}
		public static double GetWPFSize(double PixelSize)
		{
			return PixelSize * 96.0 / (double)DPI;
		}
		public static object GetObjectFromXML(XElement Str)
		{
			return GetObjectFromXML(Str.ToString());
		}
		public static object GetObjectFromXML(string Str)
		{
			Str = Str.Replace("EventType=\"", "local:CustomEventService.EventType=\"").Replace("EventData=\"", "local:CustomEventService.EventData=\"").Replace("EventType='", "local:CustomEventService.EventType='")
				.Replace("EventData='", "local:CustomEventService.EventData='")
				.Replace("Property=\"EventType\"", "Property=\"local:CustomEventService.EventType\"")
				.Replace("Property=\"EventData\"", "Property=\"local:CustomEventService.EventData\"")
				.Replace("Property='EventType'", "Property='local:CustomEventService.EventType'")
				.Replace("Property='EventData'", "Property='local:CustomEventService.EventData'");
			using MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(Str));
			using (XamlXmlReader xamlXmlReader = new XamlXmlReader((Stream)memoryStream))
			{
				while (xamlXmlReader.Read())
				{
					Type[] array = new Type[8]
					{
						typeof(System.Windows.Controls.WebBrowser),
						typeof(Frame),
						typeof(MediaElement),
						typeof(ObjectDataProvider),
						typeof(System.Xaml.XamlReader),
						typeof(Window),
						typeof(XmlDataProvider),
						typeof(SettingService)
					};
					foreach (Type type in array)
					{
						if ((object)xamlXmlReader.Type != null && type.IsAssignableFrom(xamlXmlReader.Type.UnderlyingType))
						{
							throw new UnauthorizedAccessException($"基于安全考虑，不允许使用 {type.Name} 类型。");
						}
						if (xamlXmlReader.Value != null && Operators.ConditionalCompareObjectEqual(xamlXmlReader.Value, type.Name, TextCompare: false))
						{
							throw new UnauthorizedAccessException($"基于安全考虑，不允许使用 {type.Name} 值。");
						}
					}
					string[] array2 = new string[3] { "Code", "FactoryMethod", "Static" };
					foreach (string text in array2)
					{
						if ((object)xamlXmlReader.Member != null && Operators.CompareString(xamlXmlReader.Member.Name, text, TextCompare: false) == 0)
						{
							throw new UnauthorizedAccessException($"基于安全考虑，不允许使用 {text} 成员。");
						}
					}
				}
			}
			memoryStream.Position = 0L;
			using StreamWriter streamWriter = new StreamWriter(memoryStream);
			streamWriter.Write(Str);
			streamWriter.Flush();
			memoryStream.Position = 0L;
			return System.Windows.Markup.XamlReader.Load((Stream)memoryStream);
		}
		public static bool RunInUi()
		{
			return Thread.CurrentThread.ManagedThreadId == UiThreadId;
		}
		public static bool IsVisibleInForm(this FrameworkElement element)
		{
			if (!element.IsVisible)
			{
				return false;
			}
			Rect rect = element.TransformToAncestor(ModMain.FrmMain).TransformBounds(new Rect(0.0, 0.0, element.ActualWidth, element.ActualHeight));
			Rect rect2 = new Rect(0.0, 0.0, ModMain.FrmMain.ActualWidth, ModMain.FrmMain.ActualHeight);
			return rect2.Contains(rect.TopLeft) || rect2.Contains(rect.BottomRight);
		}
		public static bool IsTextTrimmed(this TextBlock TextBlock)
		{
			Typeface typeface = new Typeface(TextBlock.FontFamily, TextBlock.FontStyle, TextBlock.FontWeight, TextBlock.FontStretch);
			return new FormattedText(TextBlock.Text, Thread.CurrentThread.CurrentCulture, TextBlock.FlowDirection, typeface, TextBlock.FontSize, TextBlock.Foreground, DPI).Width > TextBlock.ActualWidth;
		}
		public static Visibility ToVisibility(this bool IsVisible)
		{
			if (!IsVisible)
			{
				return Visibility.Collapsed;
			}
			return Visibility.Visible;
		}
		public static T RandomOne<T>(ICollection<T> objects)
		{
			return objects.ElementAtOrDefault(RandomInteger(0, checked(objects.Count - 1)));
		}
		public static int RandomInteger(int min, int max)
		{
			return checked((int)Math.Round(Math.Floor((double)(max - min + 1) * Random.NextDouble()) + (double)min));
		}
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> Raw)
		{
			List<T> list = new List<T>(Raw);
			while (list.Any())
			{
				int index = RandomInteger(0, checked(list.Count - 1));
				yield return list[index];
				list.RemoveAt(index);
			}
		}
	}
}
