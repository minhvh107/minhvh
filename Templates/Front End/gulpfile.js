var gulp = require('gulp');
var sass = require('gulp-sass');
var cssnano = require('gulp-cssnano');
var plumber = require('gulp-plumber');
var postcss = require('gulp-postcss');
var rename = require('gulp-rename');
var DEST = 'buildcss/';

/**
 * Compile Sass.
 */
gulp.task('sass', function() {
    return gulp.src('./sass/*.scss') // Create a stream in the directory where our Sass files are located.
    .pipe(sass())                    // Compile Sass into style.css.
    .pipe(gulp.dest(DEST));          // Write style.css to the project's root directory.
});

/**
 * Minify stylesheet.
 */
gulp.task('minify', ['sass'], function() {
    return gulp.src(DEST+'custom.css')  // Grab style.css and add it to the stream.
    .pipe(cssnano())               // Minify and optimize style.css
    .pipe(gulp.dest(DEST+'custom.min.css'));        // Write style.css to the project's root directory.
});

/**
 * Watch the Sass directory for changes.
 */
gulp.task('watch', function() {
  gulp.watch('./sass/*.scss', ['sass']);  // If a file changes, re-run 'sass'
});

/**
 * Minify and optimize style.css.
 */
gulp.task('cssnano', function() {
 return gulp.src(DEST+'custom.css')
 
  // Deal with errors.
 //.pipe(plumber({ errorHandler: handleErrors }))
 
 // Optimize and minify style.css.
 .pipe(cssnano({
    safe: true // Use safe optimizations.
 }))

 // Rename style.css to style.min.css.
 .pipe(rename('custom.min.css')) 

 // Write style.min.css.
 .pipe(gulp.dest(DEST));
 
 // Stream to BrowserSync.
 //.pipe(browserSync.stream());
});

// Default Task
gulp.task('default', ['sass', 'watch']);