(**
 - title: MovieClip sample
 - tagline: Basic sample implemented with fable-pixi
 - app-style: width:800px; margin:20px auto 50px auto;
 - require-paths: `'PIXI':'https://cdnjs.cloudflare.com/ajax/libs/pixi.js/3.0.11/pixi.min'`
 - intro: This is a port from [MovieClip sample](http://pixijs.github.io/examples/#/demos/movieclip-demo.js)
*)

#r "../../node_modules/fable-core/Fable.Core.dll"
#load "../../node_modules/fable-import-pixi/Fable.Import.Pixi.fs"

open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.PIXI
open Fable.Import.Browser
open Fable.Import.JS

let renderer = WebGLRenderer( 800., 600. )

let gameDiv = document.getElementById("game")
gameDiv.appendChild( renderer.view )

// create the root of the scene graph
let stage = new Container()

let rec animate (dt:float) =
  window.requestAnimationFrame(FrameRequestCallback animate) |> ignore
  // render the container
  renderer.render(stage)

let onLoad resources =

  // create an array to store the textures
  let explosionTextures = ResizeArray<Texture>()
  for i in 0..25 do
    Texture.fromFrame("Explosion_Sequence_A " + string (i+1) + ".png")  |> explosionTextures.Add

  for i in 0..49 do
    let mutable explosion = new extras.MovieClip(explosionTextures)
    explosion.position.x <- Math.random() * 800.
    explosion.position.y <- Math.random() * 600.
    explosion.anchor.x <- 0.5
    explosion.anchor.y <- 0.5
    explosion.rotation <- Math.random() * Math.PI
    explosion.scale.set(0.75 + Math.random() * 0.5)
    explosion.gotoAndPlay(Math.random() * 27.)
    stage.addChild(explosion) |> ignore

  animate 0.

Globals.loader
  .add("spritesheet","./public/assets/mc.json")
  .load(System.Func<_,_,_>(fun loader resources ->
    onLoad(resources)
  ))
