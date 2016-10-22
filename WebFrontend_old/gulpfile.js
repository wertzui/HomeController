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
    angular2: "./wwwroot/angular2/"
};

paths.assets = paths.webroot + "assets/";

//paths.js = paths.assets + "js/**/*.js";
//paths.minJs = paths.assets + "js/**/*.min.js";
//paths.css = paths.assets + "css/**/*.css";
//paths.minCss = paths.assets + "css/**/*.min.css";
//paths.concatJsDest = paths.assets + "js/site.min.js";
//paths.concatCssDest = paths.assets + "css/site.min.css";

var libsToMove = [
    paths.npmSrc + '/angular2/bundles/angular2-polyfills.js',
    paths.npmSrc + '/systemjs/dist/system.js',
    paths.npmSrc + '/systemjs/dist/system-polyfills.js',
    paths.npmSrc + '/rxjs/bundles/Rx.js',
    paths.npmSrc + '/angular2/bundles/angular2.dev.js',
    paths.npmSrc + '/angular2/bundles/http.dev.js',
    paths.npmSrc + '/angular2/bundles/router.dev.js',
    paths.npmSrc + '/es6-shim/es6-shim.min.js',

    paths.npmSrc + '/ms-signalr-client/jquery.signalr-2.2.0.js',
    paths.npmSrc + '/ms-signalr-client/node_modules/jquery/dist/jquery.js',

    paths.npmSrc + '/bootstrap/dist/css/bootstrap.css',
    paths.npmSrc + '/bootstrap/dist/js/bootstrap.js',

    paths.npmSrc + '/winjs/css/ui-dark.css',
    paths.npmSrc + '/winjs/js/base.js',
    paths.npmSrc + '/winjs/js/ui.js',

    paths.npmSrc + '/globalize/dist/globalize.js',
    paths.npmSrc + '/globalize/dist/globalize/number.js',
    paths.npmSrc + '/globalize/dist/globalize/date.js'
];
gulp.task('moveToLibs', function () {
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
