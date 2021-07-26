
	import * as THREE from '../../../build/three.module.js';

	import Stats from '../../jsm/libs/stats.module.js';

	import { OrbitControls } from '../../jsm/controls/OrbitControls.js';
	import { RoomEnvironment } from '../../jsm/environments/RoomEnvironment.js';

	import { OBJLoader } from '../../jsm/loaders/OBJLoader.js';
	import { GLTFLoader } from '../../jsm/loaders/GLTFLoader.js';
	import { DRACOLoader } from '../../jsm/loaders/DRACOLoader.js';


	if(encrypted_algo_599987 == true){
	

		//import { encrypted_algo_599987 } from 'www.arcadia-it.com/Arrera/arrera_algorithms/3d_engine.js';
		let camera, scene, renderer;
		let stats;

		let grid;
		let grid2;
		let controls;

//console.log(encrypted_algo_599987);


		function changeCategory(e,th){
if(e == 1){
$(".sidenav2").show();
$(".sidenav3").hide();
$(".sidenav4").hide();
}else if(e == 2){
    $(".sidenav2").hide();
$(".sidenav3").show();
$(".sidenav4").hide();
$("#btn_sb").click();
}else{
    $(".sidenav2").hide();
$(".sidenav3").hide();
$(".sidenav4").show();
}

$(".nav-attr").attr("style","color:white;");
$(th).attr("style","color:white;background-color:#7E0000;");
}

// var btn_sb = document.getElementById("btn_sb");
// btn_sb.addEventListener("click",(event) =>{
// event.stopPropagation();
// camera.position.set(25, 160, -45); 
// });
		const wheels = [];

		function init() {

			const container = document.getElementById('container');

			renderer = new THREE.WebGLRenderer({ antialias: true, alpha: true });
			renderer.setPixelRatio((window.devicePixelRatio) ? window.devicePixelRatio : 1);
			renderer.setSize(window.innerWidth, window.innerHeight);
			renderer.setAnimationLoop(render);
			renderer.setPixelRatio(window.devicePixelRatio);
			renderer.outputEncoding = THREE.sRGBEncoding;
			renderer.toneMapping = THREE.ACESFilmicToneMapping;
			renderer.toneMappingExposure = 0.85;
			renderer.autoClear = false;
			renderer.setClearColor(0x000000, 0.0);
			container.appendChild(renderer.domElement);

			window.addEventListener('resize', onWindowResize);

			//stats = new Stats();
			//container.appendChild( stats.dom );

			//

			camera = new THREE.PerspectiveCamera(40, window.innerWidth / window.innerHeight, 0.01, 100000);

			camera.position.set(1900, 1900, -180)

			controls = new OrbitControls(camera, container);
			controls.target.set(0, 0.5, 0);

			  controls.minDistance = 150;
			  controls.maxDistance = 150;
			 controls.minPolarAngle = -5;
			 //controls.minAzimuthAngle = 1;
			 controls.maxPolarAngle = 1.5;

			controls.update();
			const environment = new RoomEnvironment();
			const pmremGenerator = new THREE.PMREMGenerator(renderer);

			scene = new THREE.Scene();
			// scene.background = new THREE.Color( 0xD7D7D7 );
			scene.environment = pmremGenerator.fromScene(environment).texture;


            const bodyColor = new THREE.Color(0x000000);
            const interiorColor = new THREE.Color(0xff0000);
            const rimsColor = new THREE.Color(0x000000);
            const brakesColor = new THREE.Color(0x000000);
            const linesColor = new THREE.Color(0xfbff00);

            const bodyColorInput = document.getElementById("btn-colors");
            const interiorColorInput = document.getElementsByClassName("btn-interior");
            const rimsColorInput = document.getElementById("btn-rims");
            const brakesColorInput = document.getElementsByClassName("btn-brakes");
           // const linesColorInput = document.getElementById("btn-lines");

		
//             for (var i = 0; i < bodyColorInput.length; i++) {
//                 bodyColorInput[i].addEventListener('click',function(){
//         console.log(this.style.color);
//         bodyColor.set(this.style.color);
//     });
// }

    // linesColorInput.addEventListener('input',function(){
	// 				//console.log(this.value);
	// 	linesColor.set(this.value);
	
    // });

   

    bodyColorInput.addEventListener('input',function(){
					//console.log(this.value);
		bodyColor.set(this.value);
	
    });
	
	rimsColorInput.addEventListener('input',function(){
		//console.log(this.value);
		rimsColor.set(this.value);

});


			const dracoLoader = new DRACOLoader();
			dracoLoader.setDecoderPath('js/libs/draco/gltf/');

			const loader = new GLTFLoader();
			loader.setDRACOLoader(dracoLoader);
			//var v = chrome.devtools.network.getHAR(function callback);
			
			//loader.load( 'models/gltf/ARRERA SD COLOR.gltf', function ( gltf ) {

			 loader.load( 'models/gltf/06-30-2021 - PURE & PURE SPORT/pure.gltf', function ( gltf ) {
		
             const carModel = gltf.scene.children[0];
         
		
  //Body
  carModel.getObjectByName( 'Object192' ).material.color= bodyColor;
  carModel.getObjectByName( 'Object191' ).material.color= bodyColor;
  carModel.getObjectByName( 'Tube019' ).material.color= bodyColor;
  carModel.getObjectByName( 'Tube026' ).material.color= bodyColor;
  carModel.getObjectByName( 'Object173' ).material.color= bodyColor;
  carModel.getObjectByName( 'Object166' ).material.color= bodyColor;
  //Rims
   carModel.getObjectByName( 'Plane005' ).material.color= rimsColor;
  //Brakes
//   carModel.getObjectByName( 'rf' ).material.color= brakesColor;
  //Glass
  //carModel.getObjectByName( 'Object160' ).material.visible= true;
//   carModel.getObjectByName( 'Object160' ).material.color= glassColor;
  //Interior
//   carModel.getObjectByName( 'Object162' ).material.color= interiorColor;
//   carModel.getObjectByName( 'InteriorTilling001' ).material.color= interiorColor;
//   carModel.getObjectByName( 'Carbon002' ).material.color= interiorColor;
//   carModel.getObjectByName( 'SteeringWheel_Interior001' ).material.color= interiorColor;
  //Lights - Front Lights
  // carModel.getObjectByName( 'Object224' ).material.color= interiorColor;
  // carModel.getObjectByName( 'Object225' ).material.color= interiorColor;
  // carModel.getObjectByName( 'Object222' ).material.color= interiorColor;
  // carModel.getObjectByName( 'Object223' ).material.color= interiorColor;
  //Lights - Back Light
  carModel.getObjectByName( 'Plane158' ).visible= false;
  carModel.getObjectByName( 'Plane212' ).visible= false;


								  scene.add( carModel );

                            } );
       

		}

		function onWindowResize() {
			camera.aspect = window.innerWidth / window.innerHeight;
			camera.updateProjectionMatrix();
			renderer.setSize(window.innerWidth, window.innerHeight);
		}
		var SPEED = 0.01;
		function rotateCube() {
			//	wheels[0].rotation.y = 1;
			//wheels[0].rotation.y -= SPEED;
			//wheels[0].rotation.z -= SPEED * 3;
		}


		function render() {
			//requestAnimationFrame(render);
			rotateCube();
			renderer.render(scene, camera);

		}

		function animate() {

			requestAnimationFrame(animate);


			renderer.autoClear = false;
			renderer.clear();
			renderer.render(backgroundScene, backgroundCamera);
			renderer.render(scene, camera);
			controls.update();

			TWEEN.update();
			update();
		}
		init();
	}

