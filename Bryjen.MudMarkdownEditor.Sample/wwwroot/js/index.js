import {common, createStarryNight} from '@wooorm/starry-night'

window.myFunction = async function () {
    const starryNight = await createStarryNight(common)
    const tree = starryNight.highlight('"use strict";', 'source.js')
    console.log(tree)
    return starryNight;
}


