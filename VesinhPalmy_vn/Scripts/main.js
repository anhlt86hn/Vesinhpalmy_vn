function load_slide_more (o)
{
		$(o+' #vitri_11').bxSlider({
			mode: 'vertical',
			auto: true,
			speed: 2000,
		});		
}

function load_slide()
{
	/*$('#banner_L_1 .bxslider').bxSlider({
			mode: 'horizontal',
			auto: true,
			speed: 3000,
		});
		
		$('#banner_M_1 .bxslider').bxSlider({
			mode: 'vertical',
			auto: true,
			speed: 2000,
		});
		
		$('#banner_M_2 .bxslider').bxSlider({
			mode: 'horizontal',
			auto: true,
			speed: 3000,
		});
		
		$('#banner_M_3 .bxslider').bxSlider({
			mode: 'horizontal',
			auto: true,
			speed: 4000,
		});
		
		$('#banner_M_4 .bxslider').bxSlider({
			mode: 'vertical',
			auto: true,
			speed: 5000,
		});
		
		$('#banner_R_1 .bxslider').bxSlider({
			mode: 'vertical',
			auto: true,
			speed: 3000,
		});
		
		$('#banner_L_T_1 .bxslider').bxSlider({
			mode: 'horizontal',
			auto: true,
			speed: 4000,
		});
		
		$('#banner_L_T_2 .bxslider').bxSlider({
			mode: 'vertical',
			auto: true,
			speed: 3000,
		});
		
		$('#banner_L_T_3 .bxslider').bxSlider({
			mode: 'vertical',
			auto: true,
			speed: 2000,
		});
		
		$('#banner_L_T_4 .bxslider').bxSlider({
			mode: 'horizontal',
			auto: true,
			speed: 1000,
		});*/
		
		//load_slide_more (".main_left");
		//load_slide_more (".main_mid");
		
		
		
		
		$('#vitri_1').bxSlider({
			mode: 'horizontal',
			auto: true,
			pause: 5000,
		});
		
		$('#vitri_2').bxSlider({
			mode: 'vertical',
			auto: true,
			pause: 6000,
		});
		
		$('#vitri_3').bxSlider({
			mode: 'horizontal',
			auto: true,
			pause: 7000,
		});
		
		$('#vitri_4').bxSlider({
			mode: 'horizontal',
			auto: true,
			pause: 8000,
		});
		
		$('#vitri_5').bxSlider({
			mode: 'vertical',
			auto: true,
			pause: 9000,
		});
		
		$('#vitri_6').bxSlider({
			mode: 'vertical',
			auto: true,
			pause: 10000,
		});
		
		$('#vitri_7').bxSlider({
			mode: 'horizontal',
			auto: true,
			pause: 11000,
		});
		
		$('#vitri_8').bxSlider({
			mode: 'vertical',
			auto: true,
			pause: 12000,
		});
		
		$('#vitri_9').bxSlider({
			mode: 'vertical',
			auto: true,
			pause: 13000,
		});
		
		$('#vitri_10').bxSlider({
			mode: 'horizontal',
			auto: true,
			pause: 14000,
		});
		
		
		
		
		/*
		$('.bxslider').bxSlider({
			mode: 'vertical',
			auto: true,
			speed: 1000,
		});
		*/
		
		//slider = $('#banner_L_1 .bxslider, #banner_M_1 .bxslider, #banner_M_2 .bxslider, #banner_M_3 .bxslider, #banner_M_4 .bxslider, #banner_R_1 .bxslider, #banner_L_T_1 .bxslider, #banner_L_T_2 .bxslider, #banner_L_T_3 .bxslider, #banner_L_T_4 .bxslider').bxSlider();
		//slider.reloadSlider();
}


function responsive_page ()
{
	var container = $("#vnt-wrapper");
	var container_w = container.width();
	var min_width_1600 = 1580;
	var min_width_1280 = 1280;
	var min_width_1024 = 1024;
	//alert('container_w = '+container_w);
	//alert(container_w);
	if(container_w > min_width_1600)
	{
		if(!container.hasClass("max_page"))
		{
			container.addClass("max_page");
		}
	}
	else
	{
		container.removeClass("max_page");
	}
	
	if(container_w <= min_width_1600)
	{
		container.removeClass("max_page");
		if(!container.hasClass("small_page_1600"))
		{
			container.addClass("small_page_1600");
		}
	}
	else
	{
		container.removeClass("small_page_1600");
	}
	
	
	if(container_w <= min_width_1280)
	{
		container.removeClass("max_page");
		container.removeClass("small_page_1600");
		if(!container.hasClass("small_page_1280"))
		{
			container.addClass("small_page_1280");
		}
	}
	else
	{
		container.removeClass("small_page_1280");
	}
	
	if(container_w <= min_width_1024)
	{
		container.removeClass("max_page");
		container.removeClass("small_page_1600");
		container.removeClass("small_page_1280");
		if(!container.hasClass("small_page_1024"))
		{
			container.addClass("small_page_1024");
		}
	}
	else
	{
		container.removeClass("small_page_1024");
	}
	
		
	$(window).resize(function(){
		
		var container = $("#vnt-wrapper");
		var container_w = container.width();
		
		if(container_w > min_width_1600)
		{
			if(!container.hasClass("max_page"))
			{
				container.addClass("max_page");
			}
		}
		else
		{
			container.removeClass("max_page");
		}
		
		
		if(container_w <= min_width_1600)
		{
			container.removeClass("max_page");
			if(!container.hasClass("small_page_1600"))
			{
				container.addClass("small_page_1600");
			}
		}
		else
		{
			container.removeClass("small_page_1600");
		}
		
		
		if(container_w <= min_width_1280)
		{
			container.removeClass("max_page");
			container.removeClass("small_page_1600");
			if(!container.hasClass("small_page_1280"))
			{
				container.addClass("small_page_1280");
			}
		}
		else
		{
			container.removeClass("small_page_1280");
		}
		
		if(container_w <= min_width_1024 || (container_w < min_width_1280 && container_w > min_width_1024))
		{
			container.removeClass("max_page");
			container.removeClass("small_page_1600");
			container.removeClass("small_page_1280");
			if(!container.hasClass("small_page_1024"))
			{
				container.addClass("small_page_1024");
			}
		}
		else
		{
			container.removeClass("small_page_1024");
		}
		
		//slider = $('.bxslider').bxSlider();
		//slider.destroySlider();
		//load_slide();
  }); 
} 

jQuery(document).ready(function()
{
	responsive_page (); 
	load_slide();
});