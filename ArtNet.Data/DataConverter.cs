﻿using ArtNet.Data.Entities;

namespace ArtNet.Data
{
    internal static class DataConverter
    {
        internal static byte[] GetBytesFromEntity(ArtDmxEntity e)
        {
            if (e == null)
                return new byte[512];

            return new[] { e._1, e._2, e._3, e._4, e._5, e._6, e._7, e._8, e._9,
                e._10, e._11, e._12, e._13, e._14, e._15, e._16, e._17, e._18, e._19,
                e._20, e._21, e._22, e._23, e._24, e._25, e._26, e._27, e._28, e._29,
                e._30, e._31, e._32, e._33, e._34, e._35, e._36, e._37, e._38, e._39,
                e._40, e._41, e._42, e._43, e._44, e._45, e._46, e._47, e._48, e._49,
                e._50, e._51, e._52, e._53, e._54, e._55, e._56, e._57, e._58, e._59,
                e._60, e._61, e._62, e._63, e._64, e._65, e._66, e._67, e._68, e._69,
                e._70, e._71, e._72, e._73, e._74, e._75, e._76, e._77, e._78, e._79,
                e._80, e._81, e._82, e._83, e._84, e._85, e._86, e._87, e._88, e._89,
                e._90, e._91, e._92, e._93, e._94, e._95, e._96, e._97, e._98, e._99,
                e._100, e._101, e._102, e._103, e._104, e._105, e._106, e._107, e._108, e._109,
                e._110, e._111, e._112, e._113, e._114, e._115, e._116, e._117, e._118, e._119,
                e._120, e._121, e._122, e._123, e._124, e._125, e._126, e._127, e._128, e._129,
                e._130, e._131, e._132, e._133, e._134, e._135, e._136, e._137, e._138, e._139,
                e._140, e._141, e._142, e._143, e._144, e._145, e._146, e._147, e._148, e._149,
                e._150, e._151, e._152, e._153, e._154, e._155, e._156, e._157, e._158, e._159,
                e._160, e._161, e._162, e._163, e._164, e._165, e._166, e._167, e._168, e._169,
                e._170, e._171, e._172, e._173, e._174, e._175, e._176, e._177, e._178, e._179,
                e._180, e._181, e._182, e._183, e._184, e._185, e._186, e._187, e._188, e._189,
                e._190, e._191, e._192, e._193, e._194, e._195, e._196, e._197, e._198, e._199,
                e._200, e._201, e._202, e._203, e._204, e._205, e._206, e._207, e._208, e._209,
                e._210, e._211, e._212, e._213, e._214, e._215, e._216, e._217, e._218, e._219,
                e._220, e._221, e._222, e._223, e._224, e._225, e._226, e._227, e._228, e._229,
                e._230, e._231, e._232, e._233, e._234, e._235, e._236, e._237, e._238, e._239,
                e._240, e._241, e._242, e._243, e._244, e._245, e._246, e._247, e._248, e._249,
                e._250, e._251, e._252, e._253, e._254, e._255, e._256, e._257, e._258, e._259,
                e._260, e._261, e._262, e._263, e._264, e._265, e._266, e._267, e._268, e._269,
                e._270, e._271, e._272, e._273, e._274, e._275, e._276, e._277, e._278, e._279,
                e._280, e._281, e._282, e._283, e._284, e._285, e._286, e._287, e._288, e._289,
                e._290, e._291, e._292, e._293, e._294, e._295, e._296, e._297, e._298, e._299,
                e._300, e._301, e._302, e._303, e._304, e._305, e._306, e._307, e._308, e._309,
                e._310, e._311, e._312, e._313, e._314, e._315, e._316, e._317, e._318, e._319,
                e._320, e._321, e._322, e._323, e._324, e._325, e._326, e._327, e._328, e._329,
                e._330, e._331, e._332, e._333, e._334, e._335, e._336, e._337, e._338, e._339,
                e._340, e._341, e._342, e._343, e._344, e._345, e._346, e._347, e._348, e._349,
                e._350, e._351, e._352, e._353, e._354, e._355, e._356, e._357, e._358, e._359,
                e._360, e._361, e._362, e._363, e._364, e._365, e._366, e._367, e._368, e._369,
                e._370, e._371, e._372, e._373, e._374, e._375, e._376, e._377, e._378, e._379,
                e._380, e._381, e._382, e._383, e._384, e._385, e._386, e._387, e._388, e._389,
                e._390, e._391, e._392, e._393, e._394, e._395, e._396, e._397, e._398, e._399,
                e._400, e._401, e._402, e._403, e._404, e._405, e._406, e._407, e._408, e._409,
                e._410, e._411, e._412, e._413, e._414, e._415, e._416, e._417, e._418, e._419,
                e._420, e._421, e._422, e._423, e._424, e._425, e._426, e._427, e._428, e._429,
                e._430, e._431, e._432, e._433, e._434, e._435, e._436, e._437, e._438, e._439,
                e._440, e._441, e._442, e._443, e._444, e._445, e._446, e._447, e._448, e._449,
                e._450, e._451, e._452, e._453, e._454, e._455, e._456, e._457, e._458, e._459,
                e._460, e._461, e._462, e._463, e._464, e._465, e._466, e._467, e._468, e._469,
                e._470, e._471, e._472, e._473, e._474, e._475, e._476, e._477, e._478, e._479,
                e._480, e._481, e._482, e._483, e._484, e._485, e._486, e._487, e._488, e._489,
                e._490, e._491, e._492, e._493, e._494, e._495, e._496, e._497, e._498, e._499,
                e._500, e._501, e._502, e._503, e._504, e._505, e._506, e._507, e._508, e._509,
                e._510, e._511, e._512
            };
        }

        internal static void SetBytesToEntity(ArtDmxEntity e, byte[] data)
        {
            e._1 = data[0];
            e._2 = data[1];
            e._3 = data[2];
            e._4 = data[3];
            e._5 = data[4];
            e._6 = data[5];
            e._7 = data[6];
            e._8 = data[7];
            e._9 = data[8];
            e._10 = data[9];
            e._11 = data[10];
            e._12 = data[11];
            e._13 = data[12];
            e._14 = data[13];
            e._15 = data[14];
            e._16 = data[15];
            e._17 = data[16];
            e._18 = data[17];
            e._19 = data[18];
            e._20 = data[19];
            e._21 = data[20];
            e._22 = data[21];
            e._23 = data[22];
            e._24 = data[23];
            e._25 = data[24];
            e._26 = data[25];
            e._27 = data[26];
            e._28 = data[27];
            e._29 = data[28];
            e._30 = data[29];
            e._31 = data[30];
            e._32 = data[31];
            e._33 = data[32];
            e._34 = data[33];
            e._35 = data[34];
            e._36 = data[35];
            e._37 = data[36];
            e._38 = data[37];
            e._39 = data[38];
            e._40 = data[39];
            e._41 = data[40];
            e._42 = data[41];
            e._43 = data[42];
            e._44 = data[43];
            e._45 = data[44];
            e._46 = data[45];
            e._47 = data[46];
            e._48 = data[47];
            e._49 = data[48];
            e._50 = data[49];
            e._51 = data[50];
            e._52 = data[51];
            e._53 = data[52];
            e._54 = data[53];
            e._55 = data[54];
            e._56 = data[55];
            e._57 = data[56];
            e._58 = data[57];
            e._59 = data[58];
            e._60 = data[59];
            e._61 = data[60];
            e._62 = data[61];
            e._63 = data[62];
            e._64 = data[63];
            e._65 = data[64];
            e._66 = data[65];
            e._67 = data[66];
            e._68 = data[67];
            e._69 = data[68];
            e._70 = data[69];
            e._71 = data[70];
            e._72 = data[71];
            e._73 = data[72];
            e._74 = data[73];
            e._75 = data[74];
            e._76 = data[75];
            e._77 = data[76];
            e._78 = data[77];
            e._79 = data[78];
            e._80 = data[79];
            e._81 = data[80];
            e._82 = data[81];
            e._83 = data[82];
            e._84 = data[83];
            e._85 = data[84];
            e._86 = data[85];
            e._87 = data[86];
            e._88 = data[87];
            e._89 = data[88];
            e._90 = data[89];
            e._91 = data[90];
            e._92 = data[91];
            e._93 = data[92];
            e._94 = data[93];
            e._95 = data[94];
            e._96 = data[95];
            e._97 = data[96];
            e._98 = data[97];
            e._99 = data[98];
            e._100 = data[99];
            e._101 = data[100];
            e._102 = data[101];
            e._103 = data[102];
            e._104 = data[103];
            e._105 = data[104];
            e._106 = data[105];
            e._107 = data[106];
            e._108 = data[107];
            e._109 = data[108];
            e._110 = data[109];
            e._111 = data[110];
            e._112 = data[111];
            e._113 = data[112];
            e._114 = data[113];
            e._115 = data[114];
            e._116 = data[115];
            e._117 = data[116];
            e._118 = data[117];
            e._119 = data[118];
            e._120 = data[119];
            e._121 = data[120];
            e._122 = data[121];
            e._123 = data[122];
            e._124 = data[123];
            e._125 = data[124];
            e._126 = data[125];
            e._127 = data[126];
            e._128 = data[127];
            e._129 = data[128];
            e._130 = data[129];
            e._131 = data[130];
            e._132 = data[131];
            e._133 = data[132];
            e._134 = data[133];
            e._135 = data[134];
            e._136 = data[135];
            e._137 = data[136];
            e._138 = data[137];
            e._139 = data[138];
            e._140 = data[139];
            e._141 = data[140];
            e._142 = data[141];
            e._143 = data[142];
            e._144 = data[143];
            e._145 = data[144];
            e._146 = data[145];
            e._147 = data[146];
            e._148 = data[147];
            e._149 = data[148];
            e._150 = data[149];
            e._151 = data[150];
            e._152 = data[151];
            e._153 = data[152];
            e._154 = data[153];
            e._155 = data[154];
            e._156 = data[155];
            e._157 = data[156];
            e._158 = data[157];
            e._159 = data[158];
            e._160 = data[159];
            e._161 = data[160];
            e._162 = data[161];
            e._163 = data[162];
            e._164 = data[163];
            e._165 = data[164];
            e._166 = data[165];
            e._167 = data[166];
            e._168 = data[167];
            e._169 = data[168];
            e._170 = data[169];
            e._171 = data[170];
            e._172 = data[171];
            e._173 = data[172];
            e._174 = data[173];
            e._175 = data[174];
            e._176 = data[175];
            e._177 = data[176];
            e._178 = data[177];
            e._179 = data[178];
            e._180 = data[179];
            e._181 = data[180];
            e._182 = data[181];
            e._183 = data[182];
            e._184 = data[183];
            e._185 = data[184];
            e._186 = data[185];
            e._187 = data[186];
            e._188 = data[187];
            e._189 = data[188];
            e._190 = data[189];
            e._191 = data[190];
            e._192 = data[191];
            e._193 = data[192];
            e._194 = data[193];
            e._195 = data[194];
            e._196 = data[195];
            e._197 = data[196];
            e._198 = data[197];
            e._199 = data[198];
            e._200 = data[199];
            e._201 = data[200];
            e._202 = data[201];
            e._203 = data[202];
            e._204 = data[203];
            e._205 = data[204];
            e._206 = data[205];
            e._207 = data[206];
            e._208 = data[207];
            e._209 = data[208];
            e._210 = data[209];
            e._211 = data[210];
            e._212 = data[211];
            e._213 = data[212];
            e._214 = data[213];
            e._215 = data[214];
            e._216 = data[215];
            e._217 = data[216];
            e._218 = data[217];
            e._219 = data[218];
            e._220 = data[219];
            e._221 = data[220];
            e._222 = data[221];
            e._223 = data[222];
            e._224 = data[223];
            e._225 = data[224];
            e._226 = data[225];
            e._227 = data[226];
            e._228 = data[227];
            e._229 = data[228];
            e._230 = data[229];
            e._231 = data[230];
            e._232 = data[231];
            e._233 = data[232];
            e._234 = data[233];
            e._235 = data[234];
            e._236 = data[235];
            e._237 = data[236];
            e._238 = data[237];
            e._239 = data[238];
            e._240 = data[239];
            e._241 = data[240];
            e._242 = data[241];
            e._243 = data[242];
            e._244 = data[243];
            e._245 = data[244];
            e._246 = data[245];
            e._247 = data[246];
            e._248 = data[247];
            e._249 = data[248];
            e._250 = data[249];
            e._251 = data[250];
            e._252 = data[251];
            e._253 = data[252];
            e._254 = data[253];
            e._255 = data[254];
            e._256 = data[255];
            e._257 = data[256];
            e._258 = data[257];
            e._259 = data[258];
            e._260 = data[259];
            e._261 = data[260];
            e._262 = data[261];
            e._263 = data[262];
            e._264 = data[263];
            e._265 = data[264];
            e._266 = data[265];
            e._267 = data[266];
            e._268 = data[267];
            e._269 = data[268];
            e._270 = data[269];
            e._271 = data[270];
            e._272 = data[271];
            e._273 = data[272];
            e._274 = data[273];
            e._275 = data[274];
            e._276 = data[275];
            e._277 = data[276];
            e._278 = data[277];
            e._279 = data[278];
            e._280 = data[279];
            e._281 = data[280];
            e._282 = data[281];
            e._283 = data[282];
            e._284 = data[283];
            e._285 = data[284];
            e._286 = data[285];
            e._287 = data[286];
            e._288 = data[287];
            e._289 = data[288];
            e._290 = data[289];
            e._291 = data[290];
            e._292 = data[291];
            e._293 = data[292];
            e._294 = data[293];
            e._295 = data[294];
            e._296 = data[295];
            e._297 = data[296];
            e._298 = data[297];
            e._299 = data[298];
            e._300 = data[299];
            e._301 = data[300];
            e._302 = data[301];
            e._303 = data[302];
            e._304 = data[303];
            e._305 = data[304];
            e._306 = data[305];
            e._307 = data[306];
            e._308 = data[307];
            e._309 = data[308];
            e._310 = data[309];
            e._311 = data[310];
            e._312 = data[311];
            e._313 = data[312];
            e._314 = data[313];
            e._315 = data[314];
            e._316 = data[315];
            e._317 = data[316];
            e._318 = data[317];
            e._319 = data[318];
            e._320 = data[319];
            e._321 = data[320];
            e._322 = data[321];
            e._323 = data[322];
            e._324 = data[323];
            e._325 = data[324];
            e._326 = data[325];
            e._327 = data[326];
            e._328 = data[327];
            e._329 = data[328];
            e._330 = data[329];
            e._331 = data[330];
            e._332 = data[331];
            e._333 = data[332];
            e._334 = data[333];
            e._335 = data[334];
            e._336 = data[335];
            e._337 = data[336];
            e._338 = data[337];
            e._339 = data[338];
            e._340 = data[339];
            e._341 = data[340];
            e._342 = data[341];
            e._343 = data[342];
            e._344 = data[343];
            e._345 = data[344];
            e._346 = data[345];
            e._347 = data[346];
            e._348 = data[347];
            e._349 = data[348];
            e._350 = data[349];
            e._351 = data[350];
            e._352 = data[351];
            e._353 = data[352];
            e._354 = data[353];
            e._355 = data[354];
            e._356 = data[355];
            e._357 = data[356];
            e._358 = data[357];
            e._359 = data[358];
            e._360 = data[359];
            e._361 = data[360];
            e._362 = data[361];
            e._363 = data[362];
            e._364 = data[363];
            e._365 = data[364];
            e._366 = data[365];
            e._367 = data[366];
            e._368 = data[367];
            e._369 = data[368];
            e._370 = data[369];
            e._371 = data[370];
            e._372 = data[371];
            e._373 = data[372];
            e._374 = data[373];
            e._375 = data[374];
            e._376 = data[375];
            e._377 = data[376];
            e._378 = data[377];
            e._379 = data[378];
            e._380 = data[379];
            e._381 = data[380];
            e._382 = data[381];
            e._383 = data[382];
            e._384 = data[383];
            e._385 = data[384];
            e._386 = data[385];
            e._387 = data[386];
            e._388 = data[387];
            e._389 = data[388];
            e._390 = data[389];
            e._391 = data[390];
            e._392 = data[391];
            e._393 = data[392];
            e._394 = data[393];
            e._395 = data[394];
            e._396 = data[395];
            e._397 = data[396];
            e._398 = data[397];
            e._399 = data[398];
            e._400 = data[399];
            e._401 = data[400];
            e._402 = data[401];
            e._403 = data[402];
            e._404 = data[403];
            e._405 = data[404];
            e._406 = data[405];
            e._407 = data[406];
            e._408 = data[407];
            e._409 = data[408];
            e._410 = data[409];
            e._411 = data[410];
            e._412 = data[411];
            e._413 = data[412];
            e._414 = data[413];
            e._415 = data[414];
            e._416 = data[415];
            e._417 = data[416];
            e._418 = data[417];
            e._419 = data[418];
            e._420 = data[419];
            e._421 = data[420];
            e._422 = data[421];
            e._423 = data[422];
            e._424 = data[423];
            e._425 = data[424];
            e._426 = data[425];
            e._427 = data[426];
            e._428 = data[427];
            e._429 = data[428];
            e._430 = data[429];
            e._431 = data[430];
            e._432 = data[431];
            e._433 = data[432];
            e._434 = data[433];
            e._435 = data[434];
            e._436 = data[435];
            e._437 = data[436];
            e._438 = data[437];
            e._439 = data[438];
            e._440 = data[439];
            e._441 = data[440];
            e._442 = data[441];
            e._443 = data[442];
            e._444 = data[443];
            e._445 = data[444];
            e._446 = data[445];
            e._447 = data[446];
            e._448 = data[447];
            e._449 = data[448];
            e._450 = data[449];
            e._451 = data[450];
            e._452 = data[451];
            e._453 = data[452];
            e._454 = data[453];
            e._455 = data[454];
            e._456 = data[455];
            e._457 = data[456];
            e._458 = data[457];
            e._459 = data[458];
            e._460 = data[459];
            e._461 = data[460];
            e._462 = data[461];
            e._463 = data[462];
            e._464 = data[463];
            e._465 = data[464];
            e._466 = data[465];
            e._467 = data[466];
            e._468 = data[467];
            e._469 = data[468];
            e._470 = data[469];
            e._471 = data[470];
            e._472 = data[471];
            e._473 = data[472];
            e._474 = data[473];
            e._475 = data[474];
            e._476 = data[475];
            e._477 = data[476];
            e._478 = data[477];
            e._479 = data[478];
            e._480 = data[479];
            e._481 = data[480];
            e._482 = data[481];
            e._483 = data[482];
            e._484 = data[483];
            e._485 = data[484];
            e._486 = data[485];
            e._487 = data[486];
            e._488 = data[487];
            e._489 = data[488];
            e._490 = data[489];
            e._491 = data[490];
            e._492 = data[491];
            e._493 = data[492];
            e._494 = data[493];
            e._495 = data[494];
            e._496 = data[495];
            e._497 = data[496];
            e._498 = data[497];
            e._499 = data[498];
            e._500 = data[499];
            e._501 = data[500];
            e._502 = data[501];
            e._503 = data[502];
            e._504 = data[503];
            e._505 = data[504];
            e._506 = data[505];
            e._507 = data[506];
            e._508 = data[507];
            e._509 = data[508];
            e._510 = data[509];
            e._511 = data[510];
            e._512 = data[511];
        }
    }
}