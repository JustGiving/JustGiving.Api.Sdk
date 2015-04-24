var gulp = require('gulp'),
    del = require('del'),
    babel = require('gulp-babel'),
    sourcemaps = require('gulp-sourcemaps'),
    uglify = require('gulp-uglify'),
    rename = require('gulp-rename'),
    eslint = require('gulp-eslint'),
    header = require('gulp-header'),
    pkg = require('./package.json'),
    mocha = require('gulp-mocha'),
    webserver = require('gulp-webserver');

var paths = {
  scripts: ['src/**/justgiving-apiclient.js'],
  tests: ['tests/**/*.js']
};

var banner = ['/**',
  ' * <%= pkg.name %> - <%= pkg.description %>',
  ' * @version v<%= pkg.version %>',
  ' * @link <%= pkg.homepage %>',
  ' * @license <%= pkg.license %>',
  ' */',
  ''].join('\n');

gulp.task('clean', function(cb) {
  del('./dist/justgiving-apiclient*', cb);
});

gulp.task('build', ['build-full'], function() {
   return gulp.src('dist/justgiving-apiclient.js')
     .pipe(uglify())
     .pipe(rename({ extname: '.min.js' }))
     .pipe(header(banner, { pkg : pkg } ))
     .pipe(gulp.dest('dist'));
});

gulp.task('build-full', ['lint', 'clean'], function(){
  return gulp.src(paths.scripts)
    .pipe(sourcemaps.init())
    .pipe(babel({modules:'umd', loose:'es6.classes', moduleId:'JustGiving'}))
    .pipe(header(banner, { pkg : pkg } ))
    .pipe(sourcemaps.write('.'))
    .pipe(gulp.dest('dist'));
});

gulp.task('watch', function() {
  gulp.watch(paths.scripts, ['test']);
  gulp.watch(paths.tests, ['test']);
});

gulp.task('lint', function () {
  return gulp.src(paths.scripts)
    .pipe(eslint())
    .pipe(eslint.format())
    .pipe(eslint.failOnError());
});

gulp.task('test', ['build'], function() {
  return gulp.src(['tests/*.js', 'dist/justgiving-apiclient.js'], { read: false })
    .pipe(mocha());
});

gulp.task('webserver', function() {
  // eg http://localhost:8000/examples/method-samples.html or http://localhost:8000/examples/chained.knockout.html
  gulp.src('.')
    .pipe(webserver({
      livereload: true,
      directoryListing: true
    }));
});

gulp.task('default', ['watch', 'test', 'webserver'], function() {});
