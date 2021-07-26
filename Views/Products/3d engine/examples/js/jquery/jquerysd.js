
	import * as THREE from '../../../build/three.module.js';

	import Stats from '../../jsm/libs/stats.module.js';

	import { OrbitControls } from '../../jsm/controls/OrbitControls.js';
	import { RoomEnvironment } from '../../jsm/environments/RoomEnvironment.js';

	import { OBJLoader } from '../../jsm/loaders/OBJLoader.js';
	import { GLTFLoader } from '../../jsm/loaders/GLTFLoader.js';
	import { DRACOLoader } from '../../jsm/loaders/DRACOLoader.js';

	
		let camera, scene, renderer;
		let stats;

		let grid;
		let grid2;
		let controls;

		const wheels = [];

		function init() {

			const container = document.getElementById('container_3d');

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


			camera = new THREE.PerspectiveCamera(40, window.innerWidth / window.innerHeight, 0.01, 100000);

			/*camera.position.set(190, 0, -250);

			controls = new OrbitControls(camera, container);
			controls.target.set(0, 0, 0);

			  controls.minDistance = 0.25;
			  controls.maxDistance = 0.25;
			 controls.minPolarAngle = -2.2;
			 controls.minAzimuthAngle = 1;
			 controls.maxPolarAngle = 1.5;*/
			 
			 
			 camera.position.set(1, 0, -100);

controls = new OrbitControls(camera, container);
 controls.target.set(0, 0.5, 0);

   controls.minDistance = 2;
   controls.maxDistance = 2;
 controls.minPolarAngle = 1.5;
 controls.minAzimuthAngle = 1;
 controls.maxPolarAngle = 1.5;

			controls.update();
			const environment = new RoomEnvironment();
			const pmremGenerator = new THREE.PMREMGenerator(renderer);

			scene = new THREE.Scene();
			// scene.background = new THREE.Color( 0xD7D7D7 );
			scene.environment = pmremGenerator.fromScene(environment).texture;


         

			const dracoLoader = new DRACOLoader();
			dracoLoader.setDecoderPath('js/libs/draco/gltf/');

			const loader = new GLTFLoader();
			loader.setDRACOLoader(dracoLoader);
			

			 loader.load( '/3d engine/examples/models/gltf/chair.gltf', function ( gltf ) {
		
             const carModel = gltf.scene.children[0];
       

								  scene.add( carModel );

                            } );
       

		}

		function onWindowResize() {
			camera.aspect = window.innerWidth / window.innerHeight;
			camera.updateProjectionMatrix();
			renderer.setSize(window.innerWidth, window.innerHeight);
		}
	


		function render() {
		
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


