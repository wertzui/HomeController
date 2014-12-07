﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtNet.Data.Entities
{
    internal class ArtDmxEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public short Universe { get; set; }
        [Range(1, 127)]
        public byte Sequence { get; set; }

        public byte _1 { get; set; }
        public byte _2 { get; set; }
        public byte _3 { get; set; }
        public byte _4 { get; set; }
        public byte _5 { get; set; }
        public byte _6 { get; set; }
        public byte _7 { get; set; }
        public byte _8 { get; set; }
        public byte _9 { get; set; }
        public byte _10 { get; set; }
        public byte _11 { get; set; }
        public byte _12 { get; set; }
        public byte _13 { get; set; }
        public byte _14 { get; set; }
        public byte _15 { get; set; }
        public byte _16 { get; set; }
        public byte _17 { get; set; }
        public byte _18 { get; set; }
        public byte _19 { get; set; }
        public byte _20 { get; set; }
        public byte _21 { get; set; }
        public byte _22 { get; set; }
        public byte _23 { get; set; }
        public byte _24 { get; set; }
        public byte _25 { get; set; }
        public byte _26 { get; set; }
        public byte _27 { get; set; }
        public byte _28 { get; set; }
        public byte _29 { get; set; }
        public byte _30 { get; set; }
        public byte _31 { get; set; }
        public byte _32 { get; set; }
        public byte _33 { get; set; }
        public byte _34 { get; set; }
        public byte _35 { get; set; }
        public byte _36 { get; set; }
        public byte _37 { get; set; }
        public byte _38 { get; set; }
        public byte _39 { get; set; }
        public byte _40 { get; set; }
        public byte _41 { get; set; }
        public byte _42 { get; set; }
        public byte _43 { get; set; }
        public byte _44 { get; set; }
        public byte _45 { get; set; }
        public byte _46 { get; set; }
        public byte _47 { get; set; }
        public byte _48 { get; set; }
        public byte _49 { get; set; }
        public byte _50 { get; set; }
        public byte _51 { get; set; }
        public byte _52 { get; set; }
        public byte _53 { get; set; }
        public byte _54 { get; set; }
        public byte _55 { get; set; }
        public byte _56 { get; set; }
        public byte _57 { get; set; }
        public byte _58 { get; set; }
        public byte _59 { get; set; }
        public byte _60 { get; set; }
        public byte _61 { get; set; }
        public byte _62 { get; set; }
        public byte _63 { get; set; }
        public byte _64 { get; set; }
        public byte _65 { get; set; }
        public byte _66 { get; set; }
        public byte _67 { get; set; }
        public byte _68 { get; set; }
        public byte _69 { get; set; }
        public byte _70 { get; set; }
        public byte _71 { get; set; }
        public byte _72 { get; set; }
        public byte _73 { get; set; }
        public byte _74 { get; set; }
        public byte _75 { get; set; }
        public byte _76 { get; set; }
        public byte _77 { get; set; }
        public byte _78 { get; set; }
        public byte _79 { get; set; }
        public byte _80 { get; set; }
        public byte _81 { get; set; }
        public byte _82 { get; set; }
        public byte _83 { get; set; }
        public byte _84 { get; set; }
        public byte _85 { get; set; }
        public byte _86 { get; set; }
        public byte _87 { get; set; }
        public byte _88 { get; set; }
        public byte _89 { get; set; }
        public byte _90 { get; set; }
        public byte _91 { get; set; }
        public byte _92 { get; set; }
        public byte _93 { get; set; }
        public byte _94 { get; set; }
        public byte _95 { get; set; }
        public byte _96 { get; set; }
        public byte _97 { get; set; }
        public byte _98 { get; set; }
        public byte _99 { get; set; }
        public byte _100 { get; set; }
        public byte _101 { get; set; }
        public byte _102 { get; set; }
        public byte _103 { get; set; }
        public byte _104 { get; set; }
        public byte _105 { get; set; }
        public byte _106 { get; set; }
        public byte _107 { get; set; }
        public byte _108 { get; set; }
        public byte _109 { get; set; }
        public byte _110 { get; set; }
        public byte _111 { get; set; }
        public byte _112 { get; set; }
        public byte _113 { get; set; }
        public byte _114 { get; set; }
        public byte _115 { get; set; }
        public byte _116 { get; set; }
        public byte _117 { get; set; }
        public byte _118 { get; set; }
        public byte _119 { get; set; }
        public byte _120 { get; set; }
        public byte _121 { get; set; }
        public byte _122 { get; set; }
        public byte _123 { get; set; }
        public byte _124 { get; set; }
        public byte _125 { get; set; }
        public byte _126 { get; set; }
        public byte _127 { get; set; }
        public byte _128 { get; set; }
        public byte _129 { get; set; }
        public byte _130 { get; set; }
        public byte _131 { get; set; }
        public byte _132 { get; set; }
        public byte _133 { get; set; }
        public byte _134 { get; set; }
        public byte _135 { get; set; }
        public byte _136 { get; set; }
        public byte _137 { get; set; }
        public byte _138 { get; set; }
        public byte _139 { get; set; }
        public byte _140 { get; set; }
        public byte _141 { get; set; }
        public byte _142 { get; set; }
        public byte _143 { get; set; }
        public byte _144 { get; set; }
        public byte _145 { get; set; }
        public byte _146 { get; set; }
        public byte _147 { get; set; }
        public byte _148 { get; set; }
        public byte _149 { get; set; }
        public byte _150 { get; set; }
        public byte _151 { get; set; }
        public byte _152 { get; set; }
        public byte _153 { get; set; }
        public byte _154 { get; set; }
        public byte _155 { get; set; }
        public byte _156 { get; set; }
        public byte _157 { get; set; }
        public byte _158 { get; set; }
        public byte _159 { get; set; }
        public byte _160 { get; set; }
        public byte _161 { get; set; }
        public byte _162 { get; set; }
        public byte _163 { get; set; }
        public byte _164 { get; set; }
        public byte _165 { get; set; }
        public byte _166 { get; set; }
        public byte _167 { get; set; }
        public byte _168 { get; set; }
        public byte _169 { get; set; }
        public byte _170 { get; set; }
        public byte _171 { get; set; }
        public byte _172 { get; set; }
        public byte _173 { get; set; }
        public byte _174 { get; set; }
        public byte _175 { get; set; }
        public byte _176 { get; set; }
        public byte _177 { get; set; }
        public byte _178 { get; set; }
        public byte _179 { get; set; }
        public byte _180 { get; set; }
        public byte _181 { get; set; }
        public byte _182 { get; set; }
        public byte _183 { get; set; }
        public byte _184 { get; set; }
        public byte _185 { get; set; }
        public byte _186 { get; set; }
        public byte _187 { get; set; }
        public byte _188 { get; set; }
        public byte _189 { get; set; }
        public byte _190 { get; set; }
        public byte _191 { get; set; }
        public byte _192 { get; set; }
        public byte _193 { get; set; }
        public byte _194 { get; set; }
        public byte _195 { get; set; }
        public byte _196 { get; set; }
        public byte _197 { get; set; }
        public byte _198 { get; set; }
        public byte _199 { get; set; }
        public byte _200 { get; set; }
        public byte _201 { get; set; }
        public byte _202 { get; set; }
        public byte _203 { get; set; }
        public byte _204 { get; set; }
        public byte _205 { get; set; }
        public byte _206 { get; set; }
        public byte _207 { get; set; }
        public byte _208 { get; set; }
        public byte _209 { get; set; }
        public byte _210 { get; set; }
        public byte _211 { get; set; }
        public byte _212 { get; set; }
        public byte _213 { get; set; }
        public byte _214 { get; set; }
        public byte _215 { get; set; }
        public byte _216 { get; set; }
        public byte _217 { get; set; }
        public byte _218 { get; set; }
        public byte _219 { get; set; }
        public byte _220 { get; set; }
        public byte _221 { get; set; }
        public byte _222 { get; set; }
        public byte _223 { get; set; }
        public byte _224 { get; set; }
        public byte _225 { get; set; }
        public byte _226 { get; set; }
        public byte _227 { get; set; }
        public byte _228 { get; set; }
        public byte _229 { get; set; }
        public byte _230 { get; set; }
        public byte _231 { get; set; }
        public byte _232 { get; set; }
        public byte _233 { get; set; }
        public byte _234 { get; set; }
        public byte _235 { get; set; }
        public byte _236 { get; set; }
        public byte _237 { get; set; }
        public byte _238 { get; set; }
        public byte _239 { get; set; }
        public byte _240 { get; set; }
        public byte _241 { get; set; }
        public byte _242 { get; set; }
        public byte _243 { get; set; }
        public byte _244 { get; set; }
        public byte _245 { get; set; }
        public byte _246 { get; set; }
        public byte _247 { get; set; }
        public byte _248 { get; set; }
        public byte _249 { get; set; }
        public byte _250 { get; set; }
        public byte _251 { get; set; }
        public byte _252 { get; set; }
        public byte _253 { get; set; }
        public byte _254 { get; set; }
        public byte _255 { get; set; }
        public byte _256 { get; set; }
        public byte _257 { get; set; }
        public byte _258 { get; set; }
        public byte _259 { get; set; }
        public byte _260 { get; set; }
        public byte _261 { get; set; }
        public byte _262 { get; set; }
        public byte _263 { get; set; }
        public byte _264 { get; set; }
        public byte _265 { get; set; }
        public byte _266 { get; set; }
        public byte _267 { get; set; }
        public byte _268 { get; set; }
        public byte _269 { get; set; }
        public byte _270 { get; set; }
        public byte _271 { get; set; }
        public byte _272 { get; set; }
        public byte _273 { get; set; }
        public byte _274 { get; set; }
        public byte _275 { get; set; }
        public byte _276 { get; set; }
        public byte _277 { get; set; }
        public byte _278 { get; set; }
        public byte _279 { get; set; }
        public byte _280 { get; set; }
        public byte _281 { get; set; }
        public byte _282 { get; set; }
        public byte _283 { get; set; }
        public byte _284 { get; set; }
        public byte _285 { get; set; }
        public byte _286 { get; set; }
        public byte _287 { get; set; }
        public byte _288 { get; set; }
        public byte _289 { get; set; }
        public byte _290 { get; set; }
        public byte _291 { get; set; }
        public byte _292 { get; set; }
        public byte _293 { get; set; }
        public byte _294 { get; set; }
        public byte _295 { get; set; }
        public byte _296 { get; set; }
        public byte _297 { get; set; }
        public byte _298 { get; set; }
        public byte _299 { get; set; }
        public byte _300 { get; set; }
        public byte _301 { get; set; }
        public byte _302 { get; set; }
        public byte _303 { get; set; }
        public byte _304 { get; set; }
        public byte _305 { get; set; }
        public byte _306 { get; set; }
        public byte _307 { get; set; }
        public byte _308 { get; set; }
        public byte _309 { get; set; }
        public byte _310 { get; set; }
        public byte _311 { get; set; }
        public byte _312 { get; set; }
        public byte _313 { get; set; }
        public byte _314 { get; set; }
        public byte _315 { get; set; }
        public byte _316 { get; set; }
        public byte _317 { get; set; }
        public byte _318 { get; set; }
        public byte _319 { get; set; }
        public byte _320 { get; set; }
        public byte _321 { get; set; }
        public byte _322 { get; set; }
        public byte _323 { get; set; }
        public byte _324 { get; set; }
        public byte _325 { get; set; }
        public byte _326 { get; set; }
        public byte _327 { get; set; }
        public byte _328 { get; set; }
        public byte _329 { get; set; }
        public byte _330 { get; set; }
        public byte _331 { get; set; }
        public byte _332 { get; set; }
        public byte _333 { get; set; }
        public byte _334 { get; set; }
        public byte _335 { get; set; }
        public byte _336 { get; set; }
        public byte _337 { get; set; }
        public byte _338 { get; set; }
        public byte _339 { get; set; }
        public byte _340 { get; set; }
        public byte _341 { get; set; }
        public byte _342 { get; set; }
        public byte _343 { get; set; }
        public byte _344 { get; set; }
        public byte _345 { get; set; }
        public byte _346 { get; set; }
        public byte _347 { get; set; }
        public byte _348 { get; set; }
        public byte _349 { get; set; }
        public byte _350 { get; set; }
        public byte _351 { get; set; }
        public byte _352 { get; set; }
        public byte _353 { get; set; }
        public byte _354 { get; set; }
        public byte _355 { get; set; }
        public byte _356 { get; set; }
        public byte _357 { get; set; }
        public byte _358 { get; set; }
        public byte _359 { get; set; }
        public byte _360 { get; set; }
        public byte _361 { get; set; }
        public byte _362 { get; set; }
        public byte _363 { get; set; }
        public byte _364 { get; set; }
        public byte _365 { get; set; }
        public byte _366 { get; set; }
        public byte _367 { get; set; }
        public byte _368 { get; set; }
        public byte _369 { get; set; }
        public byte _370 { get; set; }
        public byte _371 { get; set; }
        public byte _372 { get; set; }
        public byte _373 { get; set; }
        public byte _374 { get; set; }
        public byte _375 { get; set; }
        public byte _376 { get; set; }
        public byte _377 { get; set; }
        public byte _378 { get; set; }
        public byte _379 { get; set; }
        public byte _380 { get; set; }
        public byte _381 { get; set; }
        public byte _382 { get; set; }
        public byte _383 { get; set; }
        public byte _384 { get; set; }
        public byte _385 { get; set; }
        public byte _386 { get; set; }
        public byte _387 { get; set; }
        public byte _388 { get; set; }
        public byte _389 { get; set; }
        public byte _390 { get; set; }
        public byte _391 { get; set; }
        public byte _392 { get; set; }
        public byte _393 { get; set; }
        public byte _394 { get; set; }
        public byte _395 { get; set; }
        public byte _396 { get; set; }
        public byte _397 { get; set; }
        public byte _398 { get; set; }
        public byte _399 { get; set; }
        public byte _400 { get; set; }
        public byte _401 { get; set; }
        public byte _402 { get; set; }
        public byte _403 { get; set; }
        public byte _404 { get; set; }
        public byte _405 { get; set; }
        public byte _406 { get; set; }
        public byte _407 { get; set; }
        public byte _408 { get; set; }
        public byte _409 { get; set; }
        public byte _410 { get; set; }
        public byte _411 { get; set; }
        public byte _412 { get; set; }
        public byte _413 { get; set; }
        public byte _414 { get; set; }
        public byte _415 { get; set; }
        public byte _416 { get; set; }
        public byte _417 { get; set; }
        public byte _418 { get; set; }
        public byte _419 { get; set; }
        public byte _420 { get; set; }
        public byte _421 { get; set; }
        public byte _422 { get; set; }
        public byte _423 { get; set; }
        public byte _424 { get; set; }
        public byte _425 { get; set; }
        public byte _426 { get; set; }
        public byte _427 { get; set; }
        public byte _428 { get; set; }
        public byte _429 { get; set; }
        public byte _430 { get; set; }
        public byte _431 { get; set; }
        public byte _432 { get; set; }
        public byte _433 { get; set; }
        public byte _434 { get; set; }
        public byte _435 { get; set; }
        public byte _436 { get; set; }
        public byte _437 { get; set; }
        public byte _438 { get; set; }
        public byte _439 { get; set; }
        public byte _440 { get; set; }
        public byte _441 { get; set; }
        public byte _442 { get; set; }
        public byte _443 { get; set; }
        public byte _444 { get; set; }
        public byte _445 { get; set; }
        public byte _446 { get; set; }
        public byte _447 { get; set; }
        public byte _448 { get; set; }
        public byte _449 { get; set; }
        public byte _450 { get; set; }
        public byte _451 { get; set; }
        public byte _452 { get; set; }
        public byte _453 { get; set; }
        public byte _454 { get; set; }
        public byte _455 { get; set; }
        public byte _456 { get; set; }
        public byte _457 { get; set; }
        public byte _458 { get; set; }
        public byte _459 { get; set; }
        public byte _460 { get; set; }
        public byte _461 { get; set; }
        public byte _462 { get; set; }
        public byte _463 { get; set; }
        public byte _464 { get; set; }
        public byte _465 { get; set; }
        public byte _466 { get; set; }
        public byte _467 { get; set; }
        public byte _468 { get; set; }
        public byte _469 { get; set; }
        public byte _470 { get; set; }
        public byte _471 { get; set; }
        public byte _472 { get; set; }
        public byte _473 { get; set; }
        public byte _474 { get; set; }
        public byte _475 { get; set; }
        public byte _476 { get; set; }
        public byte _477 { get; set; }
        public byte _478 { get; set; }
        public byte _479 { get; set; }
        public byte _480 { get; set; }
        public byte _481 { get; set; }
        public byte _482 { get; set; }
        public byte _483 { get; set; }
        public byte _484 { get; set; }
        public byte _485 { get; set; }
        public byte _486 { get; set; }
        public byte _487 { get; set; }
        public byte _488 { get; set; }
        public byte _489 { get; set; }
        public byte _490 { get; set; }
        public byte _491 { get; set; }
        public byte _492 { get; set; }
        public byte _493 { get; set; }
        public byte _494 { get; set; }
        public byte _495 { get; set; }
        public byte _496 { get; set; }
        public byte _497 { get; set; }
        public byte _498 { get; set; }
        public byte _499 { get; set; }
        public byte _500 { get; set; }
        public byte _501 { get; set; }
        public byte _502 { get; set; }
        public byte _503 { get; set; }
        public byte _504 { get; set; }
        public byte _505 { get; set; }
        public byte _506 { get; set; }
        public byte _507 { get; set; }
        public byte _508 { get; set; }
        public byte _509 { get; set; }
        public byte _510 { get; set; }
        public byte _511 { get; set; }
        public byte _512 { get; set; }

    }
}