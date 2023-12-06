$(function () {
	//header
	$(window).scroll(function () {
		var scrollTop = $(window).scrollTop();
		var $header = $('.header');
		var hedaerTop = $header.height();
		if (scrollTop >= hedaerTop) {
			$header.addClass('fixed');
		} else {
			$header.removeClass('fixed');
		}
	});

	//all menu
	$('.user_wrap .ico.menu').on('click', function () {

		if ($(this).hasClass('close')) {
			menuClose(wdSize);
		} else {
			menuOpen(wdSize);
		}
	});

	$('.all_menu .close_btn, .mobile_menu_bg').on('click', function () {
		menuClose(wdSize);
	});

	//user menu
	$('.user_wrap .user').on('click', function () {
		$('.user_menu').show();
	});
	$('html').on('click', function (e) {
		if (!$(e.target).hasClass('user')) {
			$('.user_menu').hide();
		}
		if (!$(e.target).hasClass('tip_btn')) {
			$('.tip_box').removeClass('open');
		}

	});


	var filter = "win16|win32|win64|macintel|mac|"; // PC일 경우
	if (navigator.platform) {
		//mobile
		if (filter.indexOf(navigator.platform.toLowerCase()) < 0) {
			resizeR();
			//ie 예외처리
			$('.not_ie').hide();
		} else {
			$(window).bind('load', function () { resizeR(); }); //pc
		}
	}


	function resizeR() {
		wdSize = $(window).outerWidth();

		resizeBr(wdSize);
		initSwiper(wdSize);
	}

	//모바일 2차메뉴 이벤트
	//$('.all_menu_nav .menu_list > li > a').on('click', function () {
	$(document).on("click", ".all_menu_nav .menu_depth01 > li > a", function () {
		if (wdSize <= mbSize && $(this).siblings(".menu_depth02").find("li").length > 0) {
			$(this).siblings('.menu_depth02').slideToggle(300);
			$('.menu_list > li > a').not(this).siblings('.menu_depth02').slideUp(300);
			return false;
		}
	});

	//family site event
	$('.family_wrap').hover(function () {
		$('.family_wrap > a').addClass('on');
	}, function () {
		$('.family_wrap > a').removeClass('on');
	});

	//탭메뉴 공통
	$('.tab_wrap').each(function () {

		$(this).find('.tab_ul li:eq(0)').addClass('active');
		$(this).find('.tab_ul li').on('click', function () {

			var tabIndex = $(this).index();
			var $tabCon = $(this).parents('.tab_wrap').find('.tab_con').eq(tabIndex);

			$(this).addClass('active').siblings('li').removeClass('active');
			$tabCon.fadeIn().siblings('div').hide();

		});
	});

	$('.pop_mask').on('click', function () {
		popClose();
	});

	/** 툴팁 이벤트 **/
	$('.tip_btn').on('click', function () {
		$(this).parents('.tip_box').addClass('open');
	});
	/*
	if (!$('#wrap').hasClass('mobile')) {
		$('.tip_box').on('click', function () {
			$(this).addClass('open');
		});
		$('.tip_box .ico.notice').on('click', function () {
			$(this).parents('.tip_box').addClass('open');
		});
	} else {
		$('.tip_btn').on('click', function () {
			$(this).parents('.tip_box').addClass('open');
		});
	}
	*/

	//IE 예외처리 호출
	notUseIe();
});



var mbSize = 1024;
var wdSize;

var mySwiper = undefined;


function initSwiper(size) {
	try {
		if (size <= 900) {
			$('.menu_nav').addClass('m_nav');
			$('.m_nav').removeClass('menu_nav');

			var mySwiper = new Swiper(".m_nav", {
				slidesPerView: 'auto',
				initialSlide: 0,
				spaceBetween: 35,
				centeredSlides: false,
				slideToClickedSlide: false,
				loopAdditionalSlides: 0,
				grabCursor: false,
				observer: true,
				observeParents: true,
				resizeObserver: false,
				freeMode: true,
			});
		}
		else if (size > 901 && mySwiper != undefined) {
			$('.m_nav').addClass('menu_nav');
			$('.menu_nav').removeClass('m_nav');

			mySwiper.destroy();
			mySwiper = undefined;

		}
	}
	catch (e) { }


}

function resizeBr(size) {

	if (size > mbSize) {

		$('.header').removeClass('mobile fixed');
		$('.all_menu').css({ 'display': 'none', 'left': '0' });
		$('.mobile_menu_bg, .log_btn').hide();
		$('.ico.menu').removeClass('close');
		$('.menu_depth02').show();


	} else {

		$('.header').addClass('mobile fixed');
		$('.all_menu').css({ 'display': 'inline', 'left': '-100%' });
		$('.user_menu').hide();
	}
}
//메뉴 열기 이벤트
function menuOpen(size) {

	if (size > mbSize) {
		$('.all_menu').stop().slideDown();

	} else {
		$('.mobile_menu_bg').fadeIn();
		$('.all_menu').animate({ 'left': '0' });
		$('.log_btn').animate({ 'left': '34px' });
		$('.ico.menu').addClass('close');

		//스크롤 방지
		scrollDisable();
	}
}
//메뉴 닫기 이벤트
function menuClose(size) {

	if (size > mbSize) {
		$('.all_menu').stop().slideUp();
	} else {
		$('.mobile_menu_bg').fadeOut();
		$('.all_menu, .log_btn').animate({ 'left': '-100%' });
		$('.ico.menu').removeClass('close');

		//스크롤 방지 해제
		scrollAble();
	}

}

//레이어 팝업 열기 이벤트
function popOpen(popNum) {
	var $popWrap = $('.pop_wrap').eq(popNum);
	$popWrap.show();
}

//레이어 팝업 닫기 이벤트
function popClose() {
	$('.pop_wrap').hide();
}

//인사말 샘플 팝업
function samplePop(spVal) {
	var spUrl = ['../popup/pop_GreetingSearch01.html', '../popup/pop_GreetingSearch02.html'];
	var spTitle = '인사말 샘플 팝업';
	var spOptions = 'width=602px,height=680px,top=100px,left=200px,scrollbars=yes';

	window.open(spUrl[spVal], spTitle, spOptions);
}

//확대보기
function detailFull() {
	window.open("../popup/pop_detail.html");
}

//축의금 설정 팝업
function cgMoneyPop(cgVal) {
	var cgUrl = ['../manage/manage_cg_money_setting.html', '../manage/m_manage_cg_money_setting.html'];
	var cgTitle = '축의금 설정 팝업';

	window.open(cgUrl[cgVal], cgTitle);
}

function windowPopClose() {
	window.close();
}

function fileUpload() {
	var fUrl = 'https://www.barunsoncard.com/order/common/pop_Fileupload.asp?input_name=upfile1&index=&up_dir=cs&order_seq=';
	var fTitle = '파일 업로드';
	var fOptions = 'width=602px,height=680px,top=100px,left=200px,scrollbars=yes';
	window.open(fUrl, fTitle, fOptions);
}

//스크롤 방지 이벤트
function scrollDisable() {
	$('body').addClass('scroll_off').on('scroll touchmove mousewheel', function (e) {
		e.preventDefault();
	});
}
//스크롤 방지해제 이벤트
function scrollAble() {
	$('body').removeClass('scroll_off').off('scroll touchmove mousewheel');
}

//토스트 알림 기능
function toast(select, msg, timer) {
	//alert(msg);
	var selId = $(select).attr('id');
	var $elem = $("<p>" + msg + "</p>");

	if ($('#' + selId).is(":checked")) {

		$(".toast").html($elem).show();

		$elem.slideToggle(100, function () {
			setTimeout(function () {
				$elem.fadeOut(function () {
					$(this).remove();
					$('.toast ').css('bottom', '');
				});
			}, timer);
			return false;
		});

		$('.toast').stop().animate({ 'bottom': '5%' });

	}
}

//IE 예외처리
function notUseIe() {
	var agent = navigator.userAgent.toLowerCase();
	if ((navigator.appName == 'Netscape' && agent.indexOf('trident') != -1) || (agent.indexOf("msie") != -1)) {
		// ie일 경우
		$('.not_ie').show();
	} else {
		// ie가 아닐 경우
		$('.not_ie').hide();
	}
}

function closeIe() {
	$('.not_ie').fadeOut();
}