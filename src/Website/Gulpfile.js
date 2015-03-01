var gulp = require("gulp"),
    babel = require("gulp-babel");

gulp.task("es6-module-loader", function() {
	return gulp.src("bower_components/es6-module-loader/dist/es6-module-loader.js")
		.pipe(gulp.dest("wwwroot/Content/scripts"));
});
gulp.task("systemjs", function() {
	return gulp.src("bower_components/system.js/dist/system.js")
		.pipe(gulp.dest("wwwroot/Content/scripts/"));
});
gulp.task("compile-es6", function () {
	return gulp.src("scripts/**/*.js")
	  .pipe(babel({ modules: 'system' }))
	  .pipe(gulp.dest("wwwroot/Content/scripts/"));
});
gulp.task("build-production", ["systemjs", "es6-module-loader", "compile-es6"]);