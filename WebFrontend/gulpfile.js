/// <binding AfterBuild='default' ProjectOpened='watch' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    less = require('gulp-less');

var paths = {
    webroot: "./wwwroot/",
    npmSrc: "./node_modules/",
    libTarget: "./wwwroot/lib/",
    angular2: "./wwwroot/angular2/",
    rxjs: "./wwwroot/lib/rxjs/"
};

paths.assets = paths.webroot + "assets/";

//paths.js = paths.assets + "js/**/*.js";
//paths.minJs = paths.assets + "js/**/*.min.js";
//paths.css = paths.assets + "css/**/*.css";
//paths.minCss = paths.assets + "css/**/*.min.css";
//paths.concatJsDest = paths.assets + "js/site.min.js";
//paths.concatCssDest = paths.assets + "css/site.min.css";

var libsToMove = [
    //paths.npmSrc + '/angular2/bundles/angular2-polyfills.js',
    //paths.npmSrc + '/angular2/bundles/angular2.dev.js',
    //paths.npmSrc + '/angular2/bundles/http.dev.js',
    //paths.npmSrc + '/angular2/bundles/router.dev.js',
    paths.npmSrc + '/@angular/core/bundles/core.umd.js',
    paths.npmSrc + '/@angular/common/bundles/common.umd.js',
    paths.npmSrc + '/@angular/compiler/bundles/compiler.umd.js',
    paths.npmSrc + '/@angular/platform-browser/bundles/platform-browser.umd.js',
    paths.npmSrc + '/@angular/platform-browser-dynamic/bundles/platform-browser-dynamic.umd.js',
    paths.npmSrc + '/@angular/http/bundles/http.umd.js',
    paths.npmSrc + '/@angular/router/bundles/router.umd.js',
    paths.npmSrc + '/@angular/forms/bundles/forms.umd.js',

    paths.npmSrc + '/systemjs/dist/system.js',
    paths.npmSrc + '/systemjs/dist/system-polyfills.js',
    //paths.npmSrc + '/rxjs/bundles/Rx.js',
    paths.npmSrc + '/core-js/client/shim.min.js',
    paths.npmSrc + '/zone.js/dist/zone.js',
    paths.npmSrc + '/reflect-metadata/Reflect.js',
    paths.npmSrc + '/core-js/client/shim.min.js',
    //paths.npmSrc + '/es6-shim/es6-shim.min.js',

    paths.npmSrc + '/ms-signalr-client/jquery.signalR.js',
    paths.npmSrc + '/jquery/dist/jquery.js',

    paths.npmSrc + '/bootstrap/dist/css/bootstrap.css',
    paths.npmSrc + '/bootstrap/dist/js/bootstrap.js',

    paths.npmSrc + '/winjs/css/ui-dark.css',
    paths.npmSrc + '/winjs/js/base.js',
    paths.npmSrc + '/winjs/js/ui.js',

    paths.npmSrc + '/globalize/dist/globalize.js',
    paths.npmSrc + '/globalize/dist/globalize/number.js',
    paths.npmSrc + '/globalize/dist/globalize/date.js'
];
gulp.task('moveToLibs', ['move-rxjs'], function () {
    return gulp.src(libsToMove)
        .pipe(gulp.dest(paths.libTarget));
});
gulp.task('less', function () {
    return gulp.src(paths.webroot + '**/*.less')
        //.pipe(sourcemaps.init())
        .pipe(less())
        .pipe(concat('site.css'))
        //.pipe(sourcemaps.write('./'))
        .pipe(gulp.dest(paths.webroot + "css"));
});

gulp.task('default', ['less', 'moveToLibs'], function () { });

gulp.task('watch', function () {
    gulp.watch(paths.webroot + '**/*.less', ['less']);
});

//gulp.task("clean:js", function (cb) {
//    rimraf(paths.concatJsDest, cb);
//});

//gulp.task("clean:css", function (cb) {
//    rimraf(paths.concatCssDest, cb);
//});

//gulp.task("clean", ["clean:js", "clean:css"]);

//gulp.task("min:js", function () {
//    return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
//        .pipe(concat(paths.concatJsDest))
//        .pipe(uglify())
//        .pipe(gulp.dest("."));
//});

//gulp.task("min:css", function () {
//    return gulp.src([paths.css, "!" + paths.minCss])
//        .pipe(concat(paths.concatCssDest))
//        .pipe(cssmin())
//        .pipe(gulp.dest("."));
//});

//gulp.task("min", ["min:js", "min:css"]);


// special case for rxjs as there is a bug
const Builder = require("systemjs-builder");
// SystemJS build options.
var options = {
    normalize: true,
    runtime: false,
    sourceMaps: true,
    sourceMapContents: true,
    minify: false,
    mangle: false
};
var builder = new Builder('./');
builder.config({
    paths: {
        "n:*": "node_modules/*",
        "rxjs/*": "node_modules/rxjs/*.js",
    },
    map: {
        "rxjs": "n:rxjs",
    },
    packages: {
        "rxjs": { main: "Rx.js", defaultExtension: "js" },
    }
});
gulp.task('move-rxjs', function () {
    return gulp.src(paths.npmSrc + '/rxjs/**/*.js')
        .pipe(gulp.dest(paths.rxjs));
    //builder.bundle('rxjs', paths.libTarget + 'Rx.js', options);
});

